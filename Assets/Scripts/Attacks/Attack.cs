using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public int dmg;
    public List<Effect> effects;

    public void ApplyAttack(Attackable target)
    {
        target.ModHp(CalculateNegativeDmg());
        foreach(Effect effect in effects)
        {
            if(effect.OnApply(target))
            {
                target.effects.Add(effect);
            }
        }
    }

    protected virtual int CalculateNegativeDmg()
    {
        return -dmg;
    }
}
