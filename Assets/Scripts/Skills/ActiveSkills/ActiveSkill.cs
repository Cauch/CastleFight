using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveSkill : Skill {
    public float Cooldown;
    public float BaseSkillRefreshSpeed;
    public float BaseExecutionTime;
    public float SkillRefreshSpeed;
    public float ExecutionTime;
    public bool Interrupt;

    public Targetable Caster;
    
    private float _beginingExecution;
    protected Targetable _target;

    public ActiveSkill(float range, float refreshSpeed, float executionTime, Targetable caster, Func<Targetable, bool> isValidTarget) : base(range, isValidTarget)
    {
        Cooldown = 0f;
        SkillRefreshSpeed = BaseSkillRefreshSpeed = refreshSpeed;
        BaseExecutionTime = BaseExecutionTime = executionTime;
        Caster = caster;
    }

    public override void ApplyOnTarget(Targetable target)
    {
        Interrupt = false;
        _target = target;
        _beginingExecution = Time.time;
    }

    // Kind of sketchy system
    public virtual ActiveSkill Update()
    {
        if (CheckForBreak() || CheckForCompletion() || Interrupt)
        {
            return null;
        }
        return this;
    }

    protected bool CheckForCompletion()
    {
        if(Time.time > _beginingExecution + BaseExecutionTime )
        {
            Complete();
            return true;
        }
        return false;
    }

    protected bool CheckForBreak()
    {
        if (!TargetingFunction.IsInRangeorMelee(Caster, _target, this.Range) || !IsValidTarget(_target))
        {
            Break();
            return true;
        }
        return false;
    }

    public virtual void Break()
    {
        Interrupt = true;
    }
    protected abstract void Complete();
}
