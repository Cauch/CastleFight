using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilgrim : GroundUnit
{
    private Attack attack;

    public override bool UseSkill()
    {
        return TargetingFunction.UseAttack(this, attack);
    }

    new private void Start()
    {
        base.Start();

        attack = new Attack(0.5f, 1, 0, 1.0f, 10, (Attackable attackable) => TargetingFunction.IsEnemy(this, attackable));
        _skills = new[] { attack };
    }
}
