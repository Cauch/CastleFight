using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaithBonus : IOffensiveModifier
{
    Builder _creator;
    float _max;
    float _attackPerFaith;

    public FaithBonus( Builder creator, float max, float attackPerFaith)
    {
        this._creator = creator;
        this._max = max;
        this._attackPerFaith = attackPerFaith;
    }

    public Attack ModifyAttack(Attack attack)
    {
        IronGripBuilder creator = _creator as IronGripBuilder;
        if(creator)
        {
            attack.Damage += Mathf.Min((float)creator.Faith.Value * _attackPerFaith, _max);
        }

        return attack;
    }

    public float ModifyDefense(float armor)
    {
        return armor;
    }
}
