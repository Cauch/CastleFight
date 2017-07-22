using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : Skill {
    public float duration;
    public float appliedTime;
    public float tickTime;
    public float tickSpeed;

    public override void ApplyOnTarget(Attackable target)
    {
        appliedTime = 0f;
        target.effects.Add(this);
        OnApply(target);
    }
    public abstract bool OnApply(Attackable target);
    public abstract bool OnTick(Attackable target);
    public abstract bool OnRemove(Attackable target);
}
