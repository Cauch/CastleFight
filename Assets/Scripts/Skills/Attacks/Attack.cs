using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : ActiveSkill
{
    public float Damage;
    protected List<IOffensiveModifier> _modifiers;

    public Attack(float range, float skillRefreshSpeed, Targetable caster, int dmg, List<IOffensiveModifier> modifiers, Func<Targetable, bool> isValidTarget) : base(range, skillRefreshSpeed, skillRefreshSpeed, caster, isValidTarget)
    {
        Damage = dmg;
        _modifiers = modifiers;
    }

    public Attack(float range, float skillRefreshSpeed, Targetable caster, int dmg, Func<Targetable, bool> isValidTarget) : base(range, skillRefreshSpeed, skillRefreshSpeed, caster, isValidTarget)
    {
        Damage = dmg;
        _modifiers = new List<IOffensiveModifier>();
    }

    public Attack(Attack attack) : base(attack.Range, attack.SkillRefreshSpeed, attack.SkillRefreshSpeed, attack.Caster, attack.isValidTarget)
    {
        Damage = attack.Damage;
        _modifiers = attack._modifiers; //Sketchy for a copy constructor. Not a true copy of the list
    }

    protected override void Complete()
    {
        Attackable attackable = _target as Attackable;
        Attack attack = new Attack(this);
        float armor = attackable.Armor;
        foreach(IOffensiveModifier modifier in _modifiers)
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
        attackable.ModHp(totalDmg);
    }

    protected virtual float CalculateNegativeDmg()
    {
        return -Damage;
    }
}
