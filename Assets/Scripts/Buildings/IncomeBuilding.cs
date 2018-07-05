using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class IncomeBuilding : Building {
    protected Resource _resource;
    protected float _cooldown;

    protected abstract void InitResource();

    new protected virtual void Start()
    {
        InitResource();
        base.Start();
    }

    public override void ActivateMaxLoading()
    {
        
        BuilderHelper.GetBuilderById(this.CreatorId).Resources.Where((r) => r.GetType() == _resource.GetType()).First().Add(_resource);
    }
    
    
}
