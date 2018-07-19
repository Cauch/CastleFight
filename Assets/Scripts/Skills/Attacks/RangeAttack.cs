using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : Attack
{
    public PooledEnum Projectile;

    public RangeAttack(float range, float skillRefreshSpeed, Targetable caster, int dmg, PooledEnum projectileEnum, Func<Targetable, bool> isValidTarget, List<IOffensiveModifier> modifiers = null, List<Effect> effects = null) : base(range, skillRefreshSpeed, caster, dmg, isValidTarget, modifiers, effects)
    {
        Projectile = projectileEnum;
    }

    public RangeAttack(Attack attack) : base(attack) { }

    protected override void Contact(AttackArmorContact contact)
    {
        ObjectPooler.Instance.SpawnFromPool(Projectile, Caster.transform.position, Caster.transform.rotation, new object[]{ contact });
    }
}
