using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : ActiveSkill
{
    public float Damage;
    public List<IOffensiveModifier> Modifiers;
    public List<Effect> Effects;

    public Attack(float range, float skillRefreshSpeed, Targetable caster, int dmg, Func<Targetable, bool> isValidTarget, List<IOffensiveModifier> modifiers = null, List<Effect> effects = null) : base(range, skillRefreshSpeed, skillRefreshSpeed, caster, isValidTarget)
    {
        Damage = dmg;
        Modifiers = modifiers ?? new List<IOffensiveModifier>();
        Effects = effects ?? new List<Effect>();
    }

    public Attack(Attack attack) : base(attack.Range, attack.BaseSkillRefreshSpeed, attack.BaseSkillRefreshSpeed, attack.Caster, attack.IsValidTarget)
    {
        Damage = attack.Damage;
        Modifiers = attack.Modifiers; //Sketchy for a copy constructor. Not a true copy of the list
        Effects = attack.Effects;
    }

    protected override void Complete()
    {
        Attackable attackable = _target as Attackable;
        Attack attack = new Attack(this);
        float armor = attackable.Armor;

        foreach(Effect effect in Effects)
        {
            effect.ApplyOnTarget(_target);
        }

        foreach(IOffensiveModifier modifier in Modifiers)
        {
            attack = modifier.ModifyAttack(attack);
            armor = modifier.ModifyDefense(armor);
        }

        foreach(IDefensiveModifier modifier in attackable.defensiveModifiers)
        {
            attack = modifier.ModifyAttack(attack);
            armor = modifier.ModifyDefense(armor);
        }

        float totalDmg = attack.CalculateNegativeDmg() * (1 - armor / 100f);
        attackable.AddHp(totalDmg);
    }

    protected virtual float CalculateNegativeDmg()
    {
        return -Damage;
    }
}
