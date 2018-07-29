using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : GroundUnit
{
    private Attack _attack;
    private Block _block;
    private FaithBonus _faithBonus;

    public override ActiveSkill UseSkill()
    {
        return TargetingFunction.UseSkill(this, _attack);
    }

    new private void Start()
    {
        base.Start();

        //Offensive bonuses
        
        _faithBonus = new FaithBonus(BuilderHelper.GetBuilderById(CreatorId), 100f, 0.5f);

        List<IOffensiveModifier> modifiers = new List<IOffensiveModifier>
        {
            _faithBonus,
        };

        //Defensive bonuses
        _block = new Block(10f, 1f);

        defensiveModifiers.Add(_block);

        _attack = new Attack(0, 1.0f, this, 10, (Targetable attackable) => TargetingFunction.IsEnemy(this, attackable))
        {
            Modifiers = modifiers
        };

        Skills = new[] { _attack };
    }
}
