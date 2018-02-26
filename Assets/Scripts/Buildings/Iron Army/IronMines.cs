using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IronMines : IncomeBuilding {
    public uint Value;


    protected override void InitResource()
    {
        _resource = new ResourceIron(Value);
    }
}
