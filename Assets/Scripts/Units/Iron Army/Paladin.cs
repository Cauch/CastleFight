using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : GroundUnit
{
    private Attack _attack;
    private Block _block;
    private FaithBonus _faithBonus;

    public override bool UseSkill()
    {
        return TargetingFunction.UseAttack(this, _attack);
    }

    new private void Start()
    {
        base.Start();

        //Offensive bonuses
        
        _faithBonus = new FaithBonus(Creator, 100f, 0.5f);

        List<IOffensiveModifier> modifiers = new List<IOffensiveModifier>
        {
            _faithBonus,
        };

        //Defensive bonuses
        _block = new Block(10f, 1f);

        defensiveModifiers.Add(_block);

        _attack = new Attack(1f, 1.0f, 0, 1.0f, 10, modifiers, (Attackable attackable) => TargetingFunction.IsEnemy(this, attackable));

        _skills = new[] { _attack };
    }
}
