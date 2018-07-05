using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeal : Effect
{
    private Attack _attack;
    private int _maxStacks;
    private float _cdReductionPerStackPercentage;

    public Zeal(float range, Attack attack, int maxStacks, float cdReductionPerStackPercentage) : base(range, 3600, 1, attack.IsValidTarget)
    {
        _attack = attack;
        _maxStacks = maxStacks;
        _cdReductionPerStackPercentage = cdReductionPerStackPercentage;
    }

    public override void OnApply(Targetable target)
    {
        _attack.SkillRefreshSpeed *= (1 - _cdReductionPerStackPercentage);
        _attack.ExecutionTime *= (1 - _cdReductionPerStackPercentage);
    }

    public override void OnReapply(Targetable target)
    {
        if(target.AlreadyAffected(this, _maxStacks))
        {
            target.AddEffect(this);
        }
    }

    public override void OnRemove(Targetable target)
    {
        _attack.SkillRefreshSpeed /=  (1 - _cdReductionPerStackPercentage);
        _attack.ExecutionTime /= (1 - _cdReductionPerStackPercentage);
    }

}
