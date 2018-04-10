using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PreviewBuilding : MonoBehaviour {
    const int LeftClick = 0;
    const int RightClick= 1;

    public Builder Builder;
    public GameObject BuildingTemplate;

    GameObject _buildingPreview;
    GameObject _world;
    bool _isLegitPlacement;
    Collider _planeCollider;
    PlayerConnection _playerObject;

	// Use this for initialization
	void Start () {
        _planeCollider = GameObject.FindGameObjectWithTag("Plane").GetComponent<Collider>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("PlayerConnection");
        if (gos.Any())
        {
            _playerObject = gos.Select(go => go.GetComponent<PlayerConnection>()).Where(p => p.isLocalPlayer).First();
        }
    }
	
	// Update is called once per frame
	void Update () {
        SetToMousePosition();
        UpdateLegitPlacement();
        UpdateColor();
        LookForConfirm();
	}

    void SetToMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (_planeCollider.Raycast(ray, out hitInfo, 1000f))
        {
            _buildingPreview.transform.position = hitInfo.point;
        }
    }

    void UpdateLegitPlacement()
    {
        Bounds bounds = _buildingPreview.GetComponent<Collider>().bounds;

        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.extents/2);
        Collider me = _buildingPreview.GetComponent<Collider>();
       
        foreach (Collider c in colliders)
        {
            if (!(c.GetComponent<Selectable>() == null || c.Equals(me)) || c.tag == "Obstacle") 
            {
                _isLegitPlacement = false;
                return;
            }
        }

        _isLegitPlacement = colliders.AsQueryable().Contains(BuildZone());
    }

    void UpdateColor()
    {
        Renderer[] rs = _buildingPreview.GetComponentsInChildren<Renderer>();
        Color c = _isLegitPlacement ? Color.green : Color.red;

        foreach (Renderer r in rs)
        {
            r.material.color = c;
        }
    }

    void LookForConfirm()
    {
        if (Input.GetMouseButtonUp(RightClick))
        {
            OnRightClick();
        }
        else if (Input.GetMouseButtonUp(LeftClick))
        {
            OnLeftClick();
        }
    }

    void OnLeftClick()
    {
        IBuildingCost cost = BuildingTemplate.GetComponent<IBuildingCost>();

        if(_isLegitPlacement && Builder.CanPayBuilding(cost)) {
            // Pay building
            Builder.PayBuilding(cost);
            if (_playerObject)
            {
                //Online
                _playerObject.Cmd_InstantiateBuilding(PrefabIdHelper.GoIdBuilding[BuildingTemplate], Builder.Allegiance, _buildingPreview.transform.position);
            }
            else
            {
                // Offline dev
                GameObject go = Instantiate(BuildingTemplate, _buildingPreview.transform.position, Quaternion.identity, _world.transform);
                Attackable building = go.GetComponent<Attackable>();

                building.Allegiance = Builder.Allegiance;
                building.IsActive = true;
            }
            Destroy(_buildingPreview);
            Destroy(this.gameObject);
        }
    }

    void OnRightClick()
    {
        Destroy(_buildingPreview);
        Destroy(this.gameObject);
    }

    public void Instantiate()
    {
        _world = GameObject.FindGameObjectWithTag("World");
        _buildingPreview = BuildingTemplate;
        _isLegitPlacement = false;
        _buildingPreview = Instantiate(BuildingTemplate);
        Destroy(_buildingPreview.GetComponent<Rigidbody>());
        Building preview = _buildingPreview.GetComponent<Building>();
        preview.IsActive = false;
        _buildingPreview.transform.SetParent(_world.transform);
    }

    Collider BuildZone()
    {
        Selectable s = Builder.GetComponent<Selectable>();

        Collider col = GameObject.FindGameObjectWithTag(s.Allegiance ? "BuildZone1" : "BuildZone0").GetComponent<Collider>();
    
        return col;
    }
}
