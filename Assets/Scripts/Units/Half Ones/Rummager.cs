using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rummager : GroundUnit
{
    private Harvest _harvest;
    private float _range = 0;

    public override ActiveSkill UseSkill()
    {
        if (Creator is HalfOnesBuilder)
        {
            if (TargetingFunction.IsInRangeorMelee(this, CastleHelper.GetCastle(Allegiance), _range))
            {
                _harvest.Deliver();
            }
            else if(_harvest.IsFull)
            {
                return null;
            }
            else
            {
                bool atLeastOneTargetInRange = false;

                IEnumerable<Corpse> targets = TargetingFunction.DetectSurroundings(this, _harvest.IsValidTarget).Cast<Corpse>();

                foreach (Corpse target in targets)
                {
                    bool targetInRange = TargetingFunction.IsInRangeorMelee(this, target, _harvest.Range);
                    atLeastOneTargetInRange = atLeastOneTargetInRange || targetInRange;
                    if (targetInRange)  
                    {
                        _harvest.ApplyOnTarget(target);
                        return _harvest;
                    }
                }
            }
        }
        return null;
    }

    new private void Start()
    {
        base.Start();

        _harvest = new Harvest(20u, 0.2f, this, 4f, TargetingFunction.IsCorpse);

        Skills = new ActiveSkill[] { _harvest };
    }

    public override string ToString()
    {
        return _harvest._currentBiomass.ToString();
    }

    public override Targetable MoveTarget()
    {
        if(_harvest.IsFull)
        {
            return CastleHelper.GetCastle(Allegiance);
        }
        return TargetingFunction.GetClosestTarget(this, CastleHelper.GetCastle(Allegiance), TargetingFunction.IsCorpse, DetectionRange * DetectionRange);
    }

}