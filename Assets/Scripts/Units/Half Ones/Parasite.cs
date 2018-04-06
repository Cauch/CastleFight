using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Parasite : GroundUnit
{
    private Attack _attack;
    private Infest _infest;

    public override ActiveSkill UseSkill()
    {
        bool atLeastOneTargetInRange = false;

        IEnumerable<Corpse> targets = TargetingFunction.DetectSurroundings(this, _infest.IsValidTarget).Cast<Corpse>();

        foreach (Corpse target in targets)
        {
            bool targetInRange = TargetingFunction.IsInRangeorMelee(this, target, _infest.Range);
            atLeastOneTargetInRange = atLeastOneTargetInRange || targetInRange;
            if (targetInRange)
            {
                if (target.DecompositionTime > _infest.ExecutionTime)
                {
                    _infest.ApplyOnTarget(target);
                    return _infest;
                }
            }
        }

        return TargetingFunction.UseAttack(this, _attack);
    }

    new private void Start()
    {
        base.Start();
        _infest = new Infest(0, 0, 2.5f, this, 0.3f, TargetingFunction.IsCorpse);
        _attack = new Attack(0, 1.0f / 0.8f, this, 12, (Targetable attackable) => TargetingFunction.IsEnemy(this, attackable))
        {
            Effects = new List<Effect> { new Neurotoxin(0.2f, 0.2f, 1, (Targetable attackable) => TargetingFunction.IsEnemyUnit(this, attackable)) { Duration = 3} }
        };

        Skills = new[] { _attack };
    }
}