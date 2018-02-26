using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priestess : GroundUnit
{
    private Attack attack;
    private Heal heal;

    public override bool UseSkill()
    {
        bool atLeastOneTargetInRange = false;

        List<Attackable> targets = TargetingFunction.DetectSurroundings(this, heal.isValidTarget);

        foreach (Attackable target in targets)
        {
            bool targetInRange = heal.IsUsable(this, target);
            atLeastOneTargetInRange = atLeastOneTargetInRange || targetInRange;
            if (targetInRange)
            {
                if (heal.cooldown <= 0 && target.MaxHp - target.Hp > heal.heal)
                {
                    heal.ApplyOnTarget(target);
                    heal.cooldown = 1.0f;
                    return true;
                }
            }
        }

        return TargetingFunction.UseAttack(this, attack);
    }

    new private void Start()
    {
        base.Start();

        attack = new Attack(0.5f, 1, 150, 1.0f, 14, (Attackable attackable) => TargetingFunction.IsEnemy(this, attackable));
        heal = new Heal(0f, 0.125f, 240, 1.0f, 60f, (Attackable attackable) => TargetingFunction.IsAlly(this, attackable));
            
        _skills = new Skill[] 
        {
            attack,
            heal
        };
    }
}
