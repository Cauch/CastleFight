using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Knight : GroundUnit
{
    private Attack attack;
    public override bool UseSkill()
    {
        return TargetingFunction.UseAttack(this, attack);
    }

    new private void Start()
    {
        base.Start();
        List<IOffensiveModifier> modifiers = new List<IOffensiveModifier>();

        CriticalHit criticalHit = new CriticalHit(0.2f, 2.0f);
        modifiers.Add(criticalHit);
        attack = new Attack(0.5f, 1.0f, 0, 1.0f, 45, modifiers, (Attackable attackable) => TargetingFunction.IsEnemy(this, attackable));
        _skills = new[] { attack };
    }
}
