using System;
using System.Collections.Generic;

public class Heal : ActiveSkill
{
    public float heal;
    protected List<IOffensiveModifier> modifiers;

    public Heal(float initCd, float usePerSecond, float range, float executionTime, float heal, Func<Attackable, bool> isValidTarget) : base(initCd, usePerSecond, range, executionTime, isValidTarget)
    {
        this.heal = heal;
    }

    public override void ApplyOnTarget(Attackable target)
    {
        target.ModHp((heal));
    }
}