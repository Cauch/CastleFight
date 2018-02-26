using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : ActiveSkill
{
    public float dmg;
    protected List<IOffensiveModifier> modifiers;

    public Attack(float initCd, float usePerSecond, float range, float executionTime, int dmg, Func<Attackable, bool> isValidTarget) : base(initCd, usePerSecond, range, executionTime, isValidTarget)
    {
        this.dmg = dmg;
        this.modifiers = new List<IOffensiveModifier>();
    }

    public Attack(float initCd, float usePerSecond, float range, float executionTime, int dmg, List<IOffensiveModifier> modifiers, Func<Attackable, bool> isValidTarget) : base(initCd, usePerSecond, range, executionTime, isValidTarget)
    {
        this.dmg = dmg;
        this.modifiers = modifiers;
    }

    public Attack(Attack attack) : base(attack.cooldown, attack.skillRefreshSpeed, attack.range, attack.executionTime, attack.isValidTarget)
    {
        this.dmg = attack.dmg;
        this.modifiers = attack.modifiers; //Sketchy for a copy constructor. Not a true copy of the list
    }

    public override void ApplyOnTarget(Attackable target)
    {
        Attack attack = new Attack(this);
        float armor = target.Armor;
        foreach(IOffensiveModifier modifier in modifiers)
        {
            attack = modifier.ModifyAttack(attack);
            armor = modifier.ModifyDefense(armor);
        }

        foreach(IDefensiveModifier modifier in target.defensiveModifiers)
        {
            attack = modifier.ModifyAttack(attack);
            armor = modifier.ModifyDefense(armor);
        }

        float totalDmg = attack.CalculateNegativeDmg() * (1 - armor / 100f);
        target.ModHp(totalDmg);
    }

    protected virtual float CalculateNegativeDmg()
    {
        return -dmg;
    }
}
