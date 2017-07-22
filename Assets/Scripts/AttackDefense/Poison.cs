using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Effect
{
    public int initialDmg;
    public int tickDmg;
    public int removeDmg;

    public override void ApplyOnTarget(Attackable target)
    {
        target.effects.Add(this);
        OnApply(target);
    }

    public override bool OnApply(Attackable target)
    {
        tickTime = 1;
        target.ModHp(-initialDmg);
        return true;
    }

    public override bool OnRemove(Attackable target)
    {
        target.ModHp(-removeDmg);
        return true;
    }

    public override bool OnTick(Attackable target)
    {
        target.ModHp(-tickDmg);
        return true;
    }
}
