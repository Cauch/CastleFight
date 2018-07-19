using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : ActiveSkill
{
    public struct AttackArmorContact
    {
        public Attackable Attackable;
        public Attack Attack;
        public float Armor;
    }

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
        Contact(GetAttackArmorContact());
    }

    // TODO cauch make it so this calcul happens AFTER contact.
    public AttackArmorContact GetAttackArmorContact()
    {
        Attackable attackable = _target as Attackable;
        Attack attack = new Attack(this);
        float armor = attackable.Armor;

        foreach (Effect effect in Effects)
        {
            effect.ApplyOnTarget(_target);
        }

        foreach (IOffensiveModifier modifier in Modifiers)
        {
            attack = modifier.ModifyAttack(attack);
            armor = modifier.ModifyDefense(armor);
        }

        foreach (IDefensiveModifier modifier in attackable.defensiveModifiers)
        {
            attack = modifier.ModifyAttack(attack);
            armor = modifier.ModifyDefense(armor);
        }

        return new AttackArmorContact
        {
            Attackable = attackable,
            Attack = attack,
            Armor = armor
        };
    }

    protected virtual void Contact(AttackArmorContact contact)
    {
        float totalDmg = contact.Attack.Damage * (1 - contact.Armor / 100f);
        contact.Attackable.AddHp(-totalDmg);
    }
}
