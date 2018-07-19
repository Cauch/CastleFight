using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight_Thrower : GroundUnit
{
    private Attack _attack;
    List<IOffensiveModifier> _modifiers;

    public override ActiveSkill UseSkill()
    {
        return TargetingFunction.UseAttack(this, _attack);
    }

    new private void Start()
    {
        base.Start();
        _modifiers = new List<IOffensiveModifier>();

        ArmorPierce armorPiercing = new ArmorPierce(0.6f);
        _modifiers.Add(armorPiercing);

        _attack = new RangeAttack(100, 1f / 0.3f, this, 60, PooledEnum.THROWABLE_WEIGHT, (Targetable attackable) => TargetingFunction.IsEnemy(this, attackable))
        {
            Modifiers = _modifiers
        };

        Skills = new Skill[]
        {
            _attack,
        };
    }
}