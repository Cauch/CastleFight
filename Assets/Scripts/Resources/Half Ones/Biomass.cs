using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBiomass : Resource {
    public ResourceBiomass(uint value) : base(value, "Biomass")
    {
    }

    public override void Purchase(Resource cost)
    {
        if (cost is ResourceBiomass)
        {
            Value -= cost.Value;
        }
    }
}
