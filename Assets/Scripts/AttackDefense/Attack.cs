using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : OffensiveSkill
{
    public int dmg; 

    public override void ApplyOnTarget(Attackable target)
    {
        target.ModHp(CalculateNegativeDmg());
    }

    protected virtual int CalculateNegativeDmg()
    {
        return -dmg;
    }
}
