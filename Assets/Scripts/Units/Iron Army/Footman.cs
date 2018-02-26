using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footman : GroundUnit
{
    private Attack _attack;

    public override bool UseSkill()
    {
        return TargetingFunction.UseAttack(this, _attack);
    }

    new private void Start()
    {
        base.Start();

        List<IOffensiveModifier> modifiers = new List<IOffensiveModifier>();

        ArmorPierce armorPiercing = new ArmorPierce(0.3f);
        modifiers.Add(armorPiercing);

        _attack = new Attack(0.5f, 0.8f, 0, 1.0f, 20, modifiers, (Attackable attackable) => TargetingFunction.IsEnemy(this, attackable));
        _skills = new[] { _attack };
    }
}
