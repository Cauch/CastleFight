using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class ChannelSkill : ActiveSkill
{
    protected float _tickCooldown;

    private float _lastTick;

    public ChannelSkill(float range, float refreshSpeed, float executionTime, Targetable caster, float tickSpeed, Func<Targetable, bool> isValidTarget) : base(range, refreshSpeed, executionTime, caster, isValidTarget)
    {
    }

    public override ActiveSkill Update()
    {
        CheckForTick(_target);
        return base.Update();
    }

    protected void CheckForTick(Targetable target)
    {
        if(Time.time > _lastTick + _tickCooldown)
        {
            Tick(target);
            _lastTick = Time.time;
        }
    }

    protected abstract void Tick(Targetable target);

}
