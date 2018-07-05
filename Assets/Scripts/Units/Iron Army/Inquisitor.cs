using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inquisitor : GroundUnit
{
    private Attack _attack;

    public override ActiveSkill UseSkill()
    {
        return TargetingFunction.UseAttack(this, _attack);
    }

    new private void Start()
    {
        base.Start();
        List<IOffensiveModifier> modifiers = new List<IOffensiveModifier>
        {
            new ArmorPierce(0.5f)
        };

        Zeal zeal = new Zeal(0, _attack, 20, 0.03f);
        Surrounded surrounded = new Surrounded(150, this, zeal, (Targetable t) => t is Inquisitor);

        surrounded.ApplyOnTarget(this);

        _attack = new Attack(0f, 1.0f, this, 20, (Targetable attackable) => TargetingFunction.IsEnemy(this, attackable))
        {
            Modifiers = modifiers
        };

        Skills = new[] { _attack };
    }
}
