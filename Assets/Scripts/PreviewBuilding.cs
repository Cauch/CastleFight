using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBuilding : MonoBehaviour {
    const int LEFT_CLICK = 0;
    const int RIGHT_CLICK= 1;

    public Builder builder;
    GameObject buildingPreview;
    GameObject world;
    public GameObject buildingTemplate;
    bool isLegitPlacement;
    Collider planeCollider;

	// Use this for initialization
	void Start () {
        planeCollider = GameObject.FindGameObjectWithTag("Plane").GetComponent<Collider>();
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

        if (planeCollider.Raycast(ray, out hitInfo, 1000f))
        {
            buildingPreview.transform.position = hitInfo.point;
        }
    }

    void UpdateLegitPlacement()
    {
        isLegitPlacement = true;
    }

    void UpdateColor()
    {
        Renderer[] rs = buildingPreview.GetComponentsInChildren<Renderer>();
        Color c = isLegitPlacement ? Color.green : Color.red;

        foreach (Renderer r in rs)
        {
            r.material.color = c;
        }
    }

    void LookForConfirm()
    {
        if (Input.GetMouseButtonUp(RIGHT_CLICK))
        {
            OnRightClick();
        }
        else if (Input.GetMouseButtonUp(LEFT_CLICK))
        {
            OnLeftClick();
        }
    }

    void OnLeftClick()
    {
        if(isLegitPlacement) {
            builder.LosteMoney(buildingTemplate.GetComponent<Building>().cost);
            buildingTemplate.transform.position = buildingPreview.transform.position;
            Destroy(buildingPreview);
            GameObject newBuilding = builder.InstantiateBuilding(buildingTemplate);
            newBuilding.transform.SetParent(world.transform);
            newBuilding.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            
            Destroy(buildingPreview);
            Destroy(this.gameObject);
        }
    }

    void OnRightClick()
    {
        Destroy(buildingPreview);
        Destroy(this.gameObject);
    }

    public void Instantiate()
    {
        world = GameObject.FindGameObjectWithTag("World");
        buildingPreview = buildingTemplate;
        isLegitPlacement = false;
        buildingPreview = Instantiate(buildingTemplate);
        Destroy(buildingPreview.GetComponent<Rigidbody>());
        Building preview = buildingPreview.GetComponent<Building>();
        preview.isActive = false;
        buildingPreview.transform.SetParent(world.transform);
        buildingPreview.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}
