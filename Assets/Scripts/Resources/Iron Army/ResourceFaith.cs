using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFaith : Resource
{
    public ResourceFaith(uint value) : base(value, "Faith")
    {
    }

    public override void Purchase(Resource cost){}
}
