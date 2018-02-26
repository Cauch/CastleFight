using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronMines : IncomeBuilding {
    public float Cooldown;
    public uint Value;

    protected override void InitResource()
    {
        _cooldown = Cooldown;
        _resource = new ResourceIron(Value);
    }
}
