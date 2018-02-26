using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : Attackable {
    public List<GameObject> Upgrades;

    private void Awake()
    {
        uiPanel.GetComponent<UIBuildingManager>().Building = this;
    }

    new protected void Start()
    {
        base.Start();
    }

    public void AdjustStart()
    {
        this.allegiance = this.Creator.allegiance;
    }

    new void Update()
    {
        base.Update();
    }
}
