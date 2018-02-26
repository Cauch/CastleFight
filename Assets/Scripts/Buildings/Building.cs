using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : Attackable {
    public List<GameObject> Upgrades;
    public float Loading;
    public float MaxTime;

    new protected void Awake()
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
        if (this.isActive)
        {
            Loading += Time.deltaTime;
            if (Loading > MaxTime)
            {
                ActivateMaxLoading();
                Loading = 0;
            }
        }
    }
    public abstract void ActivateMaxLoading();

    
}
