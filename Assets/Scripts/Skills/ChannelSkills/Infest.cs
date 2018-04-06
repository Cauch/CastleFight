using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infest : ActiveSkill
{
    private float _maxHpPercentage;

    public Infest(float range, float refreshSpeed, float executionTime, Targetable caster, float maxHpPercentage, Func<Targetable, bool> isValidTarget) : base(range, refreshSpeed, executionTime, caster, isValidTarget)
    {
        _maxHpPercentage = maxHpPercentage;
    }

    protected override void Complete()
    {
        Unit caster = Caster as Unit;
        Unit unit = (_target as Corpse).Unit;
        unit.Creator = caster.Creator;
        unit.MaxHp *= _maxHpPercentage;
        unit.Hp = unit.MaxHp;
        unit.Corpseless = true;
        (_target as Corpse).Resurrect();
        caster.Hp = 0;
    }
}
