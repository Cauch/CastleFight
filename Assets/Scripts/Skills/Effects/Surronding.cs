using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surrounded : Effect
{
    private Unit _caster;
    private Effect _effect;

    public Surrounded(float range, Unit caster, Effect effect, Func<Targetable, bool> IsValidSurrounding) : base(range, 3600, 1, IsValidSurrounding)
    {
        _caster = caster;
    }

    public override void OnTick(Targetable target)
    {
        IEnumerable<Targetable> targetables = TargetingFunction.DetectSurroundings(_caster, IsValidTarget);

        foreach (Targetable t in targetables)
        {
            if (IsValidTarget(target))
            {
                _caster.AddEffect(_effect);
            }
        }
    }
}
