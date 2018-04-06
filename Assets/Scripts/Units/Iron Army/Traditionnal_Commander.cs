using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traditionnal_Commander : GroundUnit
{
    private Attack attack;

    public override ActiveSkill UseSkill()
    {
        return TargetingFunction.UseAttack(this, attack);
    }

    new private void Start()
    {
        base.Start();
        List<IOffensiveModifier> modifiers = new List<IOffensiveModifier>();

        Defense def = new Defense(0.2f, (Attackable target) => TargetingFunction.IsAlly(this, target));
        Aura armorAura = new Aura(80, def);

        armorAura.ApplyOnTarget(this);

        ArmorPierce armorPiercing = new ArmorPierce(0.5f);
        modifiers.Add(armorPiercing);
        attack = new Attack(0f, 1.0f, this, 45, modifiers, (Targetable attackable) => TargetingFunction.IsEnemy(this, attackable));

        _skills = new[] { attack };
    }
}