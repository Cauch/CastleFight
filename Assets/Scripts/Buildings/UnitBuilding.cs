using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuilding : Building {
    
    public GameObject prefabUnit;
    public bool spawnAtCreation;
    Transform world;

    // Use this for initialization
    new void Start () {
        world = GameObject.FindGameObjectWithTag("World").transform;
        base.Start();
        if(spawnAtCreation && IsActive)
        {
            ProduceUnit();
        }
	}

    // Update is called once per frame

    void ProduceUnit()
    {
        GameObject unitGO = Instantiate(prefabUnit, this.transform.position, Quaternion.identity);
        Loading = 0f;

        unitGO.transform.SetParent(world.transform, false);
        unitGO.transform.position = FindRoomForUnit();
        unitGO.GetComponent<Unit>().IsActive = true;

        Unit unit = unitGO.GetComponent<Unit>();
        unit.CreatorId = CreatorId;
        unit.Allegiance = Allegiance;
        unit.AdjustStart();
    }

    Vector3 FindRoomForUnit()
    {
        return this.transform.position;
    }

    public override void ActivateMaxLoading()
    {
        ProduceUnit();
    }
}
