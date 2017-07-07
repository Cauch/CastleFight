using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuilding : Building {
    public float maxTime;
    public GameObject prefabUnit;
    Transform world;

    float loading;

    // Use this for initialization
    void Start () {
        world = GameObject.FindGameObjectWithTag("World").transform;
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
            GameObject unit = Instantiate(prefabUnit);
            loading = 0f;

            unit.transform.SetParent(world.transform, false);
            unit.transform.position = findRoomForUnit();
            unit.GetComponent<Unit>().isActive = true;
        }
    }

    Vector3 findRoomForUnit()
    {
        return this.transform.position + new Vector3 ( 30, 0, 0 );
    }

}
