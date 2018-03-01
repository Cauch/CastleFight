using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : Attackable {
    public List<GameObject> Upgrades;
    public float Loading;
    public float MaxTime;

    new protected void Start()
    {
        base.Start();
        PanelType = PanelType.BUILDING;
    }

    public void AdjustStart()
    {
        this.Allegiance = this.Creator.Allegiance;
    }

    new void Update()
    {
        
        base.Update();
        if (this.IsActive)
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
