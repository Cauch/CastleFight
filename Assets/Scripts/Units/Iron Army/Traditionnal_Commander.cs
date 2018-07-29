using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traditionnal_Commander : GroundUnit
{
    private Attack _attack;

    public override ActiveSkill UseSkill()
    {
        return TargetingFunction.UseSkill(this, _attack);
    }

    new private void Start()
    {
        base.Start();
        List<IOffensiveModifier> modifiers = new List<IOffensiveModifier>();

        Defense def = new Defense(0.2f, (Attackable target) => TargetingFunction.IsAllyUnit(this, target));
        Aura armorAura = new Aura(80, def);

        armorAura.ApplyOnTarget(this);

        ArmorPierce armorPiercing = new ArmorPierce(0.5f);
        modifiers.Add(armorPiercing);
        _attack = new Attack(0f, 1.0f, this, 45, (Targetable attackable) => TargetingFunction.IsEnemy(this, attackable))
        {
            Modifiers = modifiers
        };

        Skills = new[] { _attack };
    }
}