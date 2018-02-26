using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : PassiveSkill {
    public float duration;
    public float appliedTime;
    public float tickTime;
    public float tickSpeed;

    public Effect(float range, float duration, float tickSpeed, Func<Attackable, bool> isValidTarget) : base(range, isValidTarget)
    {
        this.duration = duration;
        this.tickTime = 0f;
        this.tickSpeed = tickSpeed;
        this.appliedTime = 0f;
    }

    public void Reset() 
    {
        appliedTime = 0f;
    }

    public override void ApplyOnTarget(Attackable target)
    {
        appliedTime = 0f;
        target.AddEffect(this);
    }

    public abstract bool OnApply(Attackable target);
    public abstract bool OnTick(Attackable target);
    public abstract bool OnRemove(Attackable target);
}
