using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Knight : GroundUnit
{
    private Attack _attack;
    public override ActiveSkill UseSkill()
    {
        return TargetingFunction.UseAttack(this, _attack);
    }

    new private void Start()
    {
        base.Start();
        List<IOffensiveModifier> modifiers = new List<IOffensiveModifier>();

        CriticalHit criticalHit = new CriticalHit(0.2f, 2.0f);
        modifiers.Add(criticalHit);
        _attack = new Attack(0, 1.0f, this, 45, (Targetable attackable) => TargetingFunction.IsEnemy(this, attackable))
        {
            Modifiers = modifiers
        };

        Skills = new[] { _attack };
    }
}
