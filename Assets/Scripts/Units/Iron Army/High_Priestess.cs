using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class High_Priestess : GroundUnit
{
    private Attack _attack;
    private MassHeal _massHeal;
    private Aura _healingAura;
    private OvertimeHeal _overtimeHeal;

    public override ActiveSkill UseSkill()
    {
        bool atLeastOneTargetInRange = false;

        IEnumerable<Attackable> targets = TargetingFunction.DetectSurroundings(this, _massHeal.isValidTarget).Cast<Attackable>();

        foreach (Attackable target in targets)
        {
            bool targetInRange = _massHeal.IsUsable(this, target);
            atLeastOneTargetInRange = atLeastOneTargetInRange || targetInRange;
            if (targetInRange)
            {
                if (_massHeal.Cooldown <= 0 && target.MaxHp - target.Hp > _massHeal.heal)
                {
                    foreach (Attackable ally in targets)
                    {
                        bool IsInAoe = Vector3.Distance(ally.transform.position, target.transform.position) < _massHeal.aoe;
                        if (IsInAoe)
                        {
                            _massHeal.ApplyOnTarget(target);
                        }
                    }
                    _massHeal.Cooldown = 1.0f;
                    return _massHeal;
                }
            }
        }

        return TargetingFunction.UseAttack(this, _attack);
    }

    new private void Start()
    {
        base.Start();

        _attack = new Attack(150, 1.0f, this, 25, (Targetable attackable) => TargetingFunction.IsEnemy(this, attackable));
        _massHeal = new MassHeal(240, 1.0f, 0.2f, this, 60f, 75f, (Targetable attackable) => TargetingFunction.IsAlly(this, attackable));
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