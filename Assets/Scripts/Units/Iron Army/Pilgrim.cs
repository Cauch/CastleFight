using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilgrim : GroundUnit
{
    private Attack attack;

    public override ActiveSkill UseSkill()
    {
        return TargetingFunction.UseAttack(this, attack);
    }

    new private void Start()
    {
        base.Start();

        attack = new Attack(0, 1.0f, this, 10, (Targetable attackable) => TargetingFunction.IsEnemy(this, attackable))
        {
            Effects = new List<Effect> { new FaithKill(new ResourceFaith(1), BuilderHelper.GetBuilderById(this.CreatorId), TargetingFunction.IsUnit) }
        };
        Skills = new[] { attack };
    }
}
