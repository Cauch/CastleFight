using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuilding : Building {
    public float maxTime;
    public GameObject prefabUnit;
    public bool spawnAtCreation;
    Transform world;

    float loading;

    // Use this for initialization
    new void Start () {
        world = GameObject.FindGameObjectWithTag("World").transform;
        base.Start();
        if(spawnAtCreation && isActive)
        {
            ProduceUnit();
        }
	}

    // Update is called once per frame
    new void Update()
    {
        if (isActive)   
        {
            base.Update();
            loading += Time.deltaTime;

            CheckUnitProduction();
        }
    }

    void CheckUnitProduction()
    {
        if(maxTime <= loading)
        {
            ProduceUnit();
        }
    }

    void ProduceUnit()
    {
        GameObject unit = Instantiate(prefabUnit, this.transform.position, Quaternion.identity);
        loading = 0f;

        unit.transform.SetParent(world.transform, false);
        unit.transform.position = FindRoomForUnit();
        unit.GetComponent<Unit>().isActive = true;

        Unit unitComp = unit.GetComponent<Unit>();
        unitComp.Creator = Creator;
        unitComp.AdjustStart();
    }

    Vector3 FindRoomForUnit()
    {
        return this.transform.position;
    }

}
