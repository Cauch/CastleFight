using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : PassiveSkill {
    public float duration;
    public float appliedTime;
    public float tickTime;
    public float tickSpeed;

    public Effect(float range, float duration, float tickSpeed, Func<Targetable, bool> isValidTarget) : base(range, isValidTarget)
    {
        this.duration = duration;
        this.tickTime = 0f;
        this.tickSpeed = tickSpeed;
        this.appliedTime = 0f;
    }

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

    public override void ApplyOnTarget(Targetable target)
    {
        appliedTime = 0f;
        target.AddEffect(this);
    }

    //Temporary mesure to not rewrite everything... should be redone to only take in consideration Targetable (abstract not virtual)
    public virtual bool OnApply(Attackable target) { return true; }
    public virtual bool OnTick(Attackable target)  { return true; }
    public virtual bool OnRemove(Attackable target) { return true; }
    public virtual bool OnApply(Targetable target) {return OnApply(target as Attackable); }
    public virtual bool OnTick(Targetable target) { return OnTick(target as Attackable); }
    public virtual bool OnRemove(Targetable target) { return OnRemove(target as Attackable); }
}
