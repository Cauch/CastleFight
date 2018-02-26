using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class IncomeBuilding : Building {
    protected IResource _resource;
    protected float _cooldown;
    private float _time;

    protected abstract void InitResource();

    new protected virtual void Start()
    {
        InitResource();
        base.Start();
    }

    public override void ActivateMaxLoading()
    {
        Creator.Resources.Where((r) => r.GetType() == _resource.GetType()).First().Add(_resource);
    }
    
    
}
