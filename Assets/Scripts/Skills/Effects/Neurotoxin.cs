using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Neurotoxin : Effect
{
    private float _movementSpeedReduction;
    private float _skillSpeedReduction;
    private uint _maxStack;

    public Neurotoxin(float movementSpeedReduction, float skillSpeedReduction, uint maxStack, Func<Targetable, bool> IsValidTarget) : base(0, 1, 1, IsValidTarget)
    {
        _movementSpeedReduction = movementSpeedReduction;
        _skillSpeedReduction = skillSpeedReduction;
        _maxStack = maxStack;
    }

    public override void OnApply(Targetable target)
    {
        Unit unit = (target as Unit);

        foreach(ActiveSkill s in unit.Skills)
        {
            s.SkillRefreshSpeed *= (1 - _skillSpeedReduction);
        }

        unit.SetSpeed(unit.DefaultSpeed * (1 - _movementSpeedReduction));
    }

    public override void OnRemove(Targetable target)
    {
        Unit unit = (target as Unit);

        foreach (ActiveSkill s in unit.Skills)
        {
            s.SkillRefreshSpeed /= (1 - _skillSpeedReduction);
        }

        unit.SetSpeed(unit.DefaultSpeed);
    }
}
