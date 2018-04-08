using System;
using System.Collections.Generic;

public class MassHeal : ActiveSkill
{
    public float heal;
    public float aoe;

    public MassHeal(float range, float refrehSpeed, float executionTime, Targetable caster, float heal, float aoe, Func<Targetable, bool> isValidTarget) : base(range, refrehSpeed, executionTime, caster, isValidTarget)
    {
        this.heal = heal;
        this.aoe = aoe;
    }

    protected override void Complete()
    {
        (_target as Attackable).AddHp((heal));
    }
}