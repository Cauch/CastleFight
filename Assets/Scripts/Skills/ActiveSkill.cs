using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveSkill : Skill {
    public float cooldown;
    public float skillRefreshSpeed;
    public float executionTime;

    public ActiveSkill(float initCd, float usePerSeconds, float range, float executionTime, Func<Attackable, bool> isValidTarget) : base(range, isValidTarget)
    {
        this.cooldown = initCd;
        this.skillRefreshSpeed = usePerSeconds;
        this.executionTime = executionTime;
    }
}
