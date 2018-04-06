using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveSkill : Skill {
    public float Cooldown;
    public float SkillRefreshSpeed;
    public float ExecutionTime;

    public Targetable Caster;
    
    private float _beginingExecution;
    protected Targetable _target;

    public ActiveSkill(float range, float refreshSpeed, float executionTime, Targetable caster, Func<Targetable, bool> isValidTarget) : base(range, isValidTarget)
    {
        this.Cooldown = 0f;
        this.SkillRefreshSpeed = refreshSpeed;
        this.ExecutionTime = executionTime;
        Caster = caster;
    }

    public override void ApplyOnTarget(Targetable target)
    {
        _target = target;
        _beginingExecution = Time.time;
    }

    // Kind of sketchy system
    public virtual ActiveSkill Update()
    {
        if (CheckForBreak() || CheckForCompletion())
        {
            return null;
        }
        return this;
    }

    protected bool CheckForCompletion()
    {
        if(Time.time > _beginingExecution + ExecutionTime )
        {
            Complete();
            return true;
        }
        return false;
    }

    protected bool CheckForBreak()
    {
        if (!TargetingFunction.IsInRangeorMelee(Caster, _target, this.Range) || !isValidTarget(_target))
        {
            Break();
            return true;
        }
        return false;
    }

    public virtual void Break()
    {

    }
    protected abstract void Complete();
}
