using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FaithKill : Effect {
    ResourceFaith _faith;
    Builder _builder;

    public FaithKill(ResourceFaith gain, Builder builder, Func<Attackable, bool> IsValidTarget) : base(0, 1, 1, IsValidTarget)
    {
    }

    public override OnDeath(Targetable target)
    {
        IEnumerable<ResourceFaith> resourcesFaith = _builder.Resources.OfType<ResourceFaith>();
        if (resourcesFaith.Any<ResourceFaith>())
        {
            resourcesFaith.First().Add(_faith);
        }
    }
}
