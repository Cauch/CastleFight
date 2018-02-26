using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traditionnal_Knight : GroundUnit
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

        ArmorPierce armorPiercing = new ArmorPierce(0.5f);
        modifiers.Add(armorPiercing);
        attack = new Attack(0.5f, 0.8f, 0, 1.0f, 40, modifiers, (Attackable attackable) => TargetingFunction.IsEnemy(this, attackable));
        _skills = new[] { attack };
    }
}
