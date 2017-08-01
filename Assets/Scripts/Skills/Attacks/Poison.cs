using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Effect
{
    protected int initialDmg;
    protected int tickDmg;
    protected int removeDmg;

    public Poison(float initCd, float usePerSecond, float range, float executionTime, float duration, float tickSpeed, int initialDmg, int tickDmg, int removeDmg) : base(initCd, usePerSecond, range, executionTime, duration, tickSpeed)
    {
        this.initialDmg = initialDmg;
        this.tickDmg = tickDmg;
        this.removeDmg = removeDmg;
    }

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
