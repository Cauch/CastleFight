using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : Attackable {
    public float income;
    public uint cost;

    protected void Start()
    {
        uiPanel.GetComponent<UIBuildingManager>().building = this;
    }

    new void Update()
    {
        base.Update();
    }
}
