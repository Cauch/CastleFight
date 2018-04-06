using System;
using System.Collections.Generic;

public class Heal : ActiveSkill
{
    public float heal;
    protected List<IOffensiveModifier> modifiers;

    public Heal(float range, float refrehSpeed, float executionTime, Targetable caster, float heal, Func<Targetable, bool> isValidTarget) : base(range, refrehSpeed, executionTime, caster, isValidTarget)
    {
        this.heal = heal;
    }

    protected override void Complete()
    {
        (_target as Attackable).AddHp((heal));
    }
}