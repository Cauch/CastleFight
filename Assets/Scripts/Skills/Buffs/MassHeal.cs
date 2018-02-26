using System;
using System.Collections.Generic;

public class MassHeal : ActiveSkill
{
    public float heal;
    public float aoe;
    protected List<IOffensiveModifier> modifiers;

    public MassHeal(float initCd, float usePerSecond, float range, float executionTime, float heal, float aoe, Func<Attackable, bool> isValidTarget) : base(initCd, usePerSecond, range, executionTime, isValidTarget)
    {
        this.heal = heal;
        this.aoe = aoe;
    }

    public override void ApplyOnTarget(Attackable target)
    {
        target.ModHp((heal));
    }
}