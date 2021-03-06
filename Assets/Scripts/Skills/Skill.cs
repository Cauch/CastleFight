﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill {
    const int BATTLE_FIELD_LAYER_MASK = 8;

    public float Range;
    public Func<Targetable, bool> IsValidTarget;

    public Skill(float range, Func<Targetable, bool> isValidTarget)
    {
        this.Range = range;
        this.IsValidTarget = isValidTarget;
    }

    public Skill(float range, Func<Attackable, bool> isValidTarget)
    {
        this.Range = range;
        this.IsValidTarget = TargetingFunction.ToTargetableFunc(isValidTarget);
    }

    public abstract void ApplyOnTarget(Targetable target);

    public bool IsUsable(Attackable attacker, Attackable target)
    {
        return (IsMelee(attacker, target) || TargetingFunction.IsInRangeorMelee(attacker, target, Range)) && IsValidTarget(target);
    }

    bool IsMelee(Attackable attacker, Attackable target)
    {
        Bounds aBounds = attacker.GetComponent<Collider>().bounds;
        Bounds tBounds = target.GetComponent<Collider>().bounds;

        return tBounds.Intersects(aBounds);
    }
}
