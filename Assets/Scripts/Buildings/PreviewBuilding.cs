using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PreviewBuilding : MonoBehaviour {
    const int LeftClick = 0;
    const int RightClick= 1;

    public Builder Builder;
    GameObject _buildingPreview;
    GameObject _world;
    public GameObject BuildingTemplate;
    bool _isLegitPlacement;
    Collider _planeCollider;

	// Use this for initialization
	void Start () {
        _planeCollider = GameObject.FindGameObjectWithTag("Plane").GetComponent<Collider>();
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
            BuildingTemplate.transform.position = _buildingPreview.transform.position;
            Destroy(_buildingPreview);
            GameObject newBuilding = Builder.InstantiateBuilding(BuildingTemplate);
            newBuilding.transform.SetParent(_world.transform);
            newBuilding.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            newBuilding.GetComponent<Building>().Creator = Builder;
            newBuilding.GetComponent<Building>().allegiance = Builder.allegiance;
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
        preview.isActive = false;
        _buildingPreview.transform.SetParent(_world.transform);
        _buildingPreview.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    Collider BuildZone()
    {
        Selectable s = Builder.GetComponent<Selectable>();

        Collider col = GameObject.FindGameObjectWithTag(s.allegiance ? "BuildZone1" : "BuildZone0").GetComponent<Collider>();
    
        return col;
    }
}
