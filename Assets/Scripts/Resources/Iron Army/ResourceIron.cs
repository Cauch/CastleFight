using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceIron : Resource
{
    public ResourceIron(uint value) : base(value, "Iron")
    {
    }

    public override void Purchase(Resource cost)
    {
        if (cost is ResourceIron)
        {
            Value -= cost.Value;
        }
    }
}
