using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : OffensiveSkill
{
    protected int dmg; 

    public Attack(float initCd, float usePerSecond, float range, float executionTime, int dmg) : base(initCd, usePerSecond, range, executionTime)
    {
        this.dmg = dmg;
    }

    public override void ApplyOnTarget(Attackable target)
    {
        target.ModHp(CalculateNegativeDmg() * 1 - target.armor/100);
    }

    protected virtual int CalculateNegativeDmg()
    {
        return -dmg;
    }
}
