using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class IncomeBuilding : Building {
    protected IResource _resource;
    protected float _cooldown;
    private float _time;

    protected abstract void InitResource();
    private void Awake()
    {
        uiPanel.GetComponent<UIBuildingManager>().Building = this;
    }

    new protected virtual void Start()
    {
        InitResource();
        base.Start();
    }

    new protected virtual void Update()
    {
        base.Update();

        _time += Time.deltaTime;

        if (_time > _cooldown / GameSettings.IncomeSpeedModifier)
        {
            Creator.Resources.Where((r) => r.GetType() == _resource.GetType()).First().Add(_resource);
            _time = 0;
        }
    }
}
