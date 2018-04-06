using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBiomass : IResource {
    public ResourceBiomass(uint value) : base(value, "Biomass")
    {
    }

    public override void Purchase(IResource cost)
    {
        if (cost is ResourceBiomass)
        {
            Value -= cost.Value;
        }
    }
}
