using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : Attackable {
    public float income;
    public uint cost;

    new protected void Start()
    {
        base.Start();
        uiPanel.GetComponent<UIBuildingManager>().building = this;
    }

    public void AdjustStart()
    {
        this.allegiance = this.creator.allegiance;
    }

    new void Update()
    {
        base.Update();
    }
}
