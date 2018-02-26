using System.Collections.Generic;
using UnityEngine;

public class High_Priestess : GroundUnit
{
    private Attack _attack;
    private MassHeal _massHeal;
    private Aura _healingAura;
    private OvertimeHeal _overtimeHeal;

    public override bool UseSkill()
    {
        bool atLeastOneTargetInRange = false;

        List<Attackable> targets = TargetingFunction.DetectSurroundings(this, _massHeal.isValidTarget);

        foreach (Attackable target in targets)
        {
            bool targetInRange = _massHeal.IsUsable(this, target);
            atLeastOneTargetInRange = atLeastOneTargetInRange || targetInRange;
            if (targetInRange)
            {
                if (_massHeal.cooldown <= 0 && target.MaxHp - target.Hp > _massHeal.heal)
                {
                    foreach (Attackable ally in targets)
                    {
                        bool IsInAoe = Vector3.Distance(ally.transform.position, target.transform.position) < _massHeal.aoe;
                        if (IsInAoe)
                        {
                            _massHeal.ApplyOnTarget(target);
                        }
                    }
                    _massHeal.cooldown = 1.0f;
                    return true;
                }
            }
        }

        return TargetingFunction.UseAttack(this, _attack);
    }

    new private void Start()
    {
        base.Start();

        _attack = new Attack(0.5f, 1, 150, 1.0f, 25, (Attackable attackable) => TargetingFunction.IsEnemy(this, attackable));
        _massHeal = new MassHeal(0f, 0.125f, 240, 1.0f, 60f, 75f, (Attackable attackable) => TargetingFunction.IsAlly(this, attackable));
        _overtimeHeal = new OvertimeHeal(2f, (Attackable attackable) => TargetingFunction.IsAlly(this, attackable));
        _healingAura = new Aura(75f, _overtimeHeal);

        _healingAura.ApplyOnTarget(this);

        _skills = new Skill[]
        {
            _attack,
            _massHeal
        };
    }
}