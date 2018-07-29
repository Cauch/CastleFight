using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Priestess : GroundUnit
{
    private Attack _attack;
    private Heal _heal;

    public override ActiveSkill UseSkill()
    {
        bool atLeastOneTargetInRange = false;

        IEnumerable<Attackable> targets = TargetingFunction.DetectSurroundings(this, _heal.IsValidTarget).Cast<Attackable>();

        foreach (Attackable target in targets)
        {
            bool targetInRange = _heal.IsUsable(this, target);
            atLeastOneTargetInRange = atLeastOneTargetInRange || targetInRange;
            if (targetInRange)
            {
                if (_heal.Cooldown <= 0 && target.MaxHp - target.Hp > _heal.heal)
                {
                    _heal.ApplyOnTarget(target);
                    _heal.Cooldown = 1.0f;
                    return _heal;
                }
            }
        }

        return TargetingFunction.UseSkill(this, _attack);
    }

    new private void Start()
    {
        base.Start();

        _attack = new Attack(150, 1.0f, this, 14, (Targetable attackable) => TargetingFunction.IsEnemy(this, attackable));
        _heal = new Heal(240, 1.0f, 0.2f, this, 60f, (Targetable attackable) => TargetingFunction.IsAllyUnit(this, attackable));
            
        Skills = new Skill[] 
        {
            _attack,
            _heal
        };
    }
}
