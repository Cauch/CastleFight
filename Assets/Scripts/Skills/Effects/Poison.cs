using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Effect
{
    protected int initialDmg;
    protected int tickDmg;
    protected int removeDmg;

    public Poison(float range, float executionTime, float duration, float tickSpeed, int initialDmg, int tickDmg, int removeDmg, Func<Attackable, bool> isValidTarget) : base(range, duration, tickSpeed, isValidTarget)
    {
        this.initialDmg = initialDmg;
        this.tickDmg = tickDmg;
        this.removeDmg = removeDmg;
    }

    public override void ApplyOnTarget(Targetable target)
    {
        target.AddEffect(this);
    }

    public override void OnApply(Targetable target)
    {
        Attackable attackable = target as Attackable;
        TickTime = 1;
        attackable.AddHp(-initialDmg);
    }

    public override void OnRemove(Targetable target)
    {
        Attackable attackable = target as Attackable;
        attackable.AddHp(-removeDmg);
    }

    public override void OnTick(Targetable target)
    {
        Attackable attackable = target as Attackable;
        attackable.AddHp(-tickDmg);
    }
}
