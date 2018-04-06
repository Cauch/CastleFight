using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : PassiveSkill {
    public float Duration;
    public float AppliedTime;
    public float TickTime;
    public float TickSpeed;

    public Effect(float range, float duration, float tickSpeed, Func<Targetable, bool> isValidTarget) : base(range, isValidTarget)
    {
        this.Duration = duration;
        this.TickTime = 0f;
        this.TickSpeed = tickSpeed;
        this.AppliedTime = 0f;
    }

    public Effect(float range, float duration, float tickSpeed, Func<Attackable, bool> isValidTarget) : base(range, isValidTarget)
    {
        this.Duration = duration;
        this.TickTime = 0f;
        this.TickSpeed = tickSpeed;
        this.AppliedTime = 0f;
    }

    public void Reset() 
    {
        AppliedTime = 0f;
    }

    public override void ApplyOnTarget(Targetable target)
    {
        AppliedTime = 0f;
        target.AddEffect(this);
    }

    //Temporary mesure to not rewrite everything... should be redone to only take in consideration Targetable (abstract not virtual)
    public virtual void OnApply(Targetable target) {}
    public virtual void OnTick(Targetable target) {  }
    public virtual void OnRemove(Targetable target) { }
    public virtual void OnReapply(Targetable target) { Reset(); }
}
