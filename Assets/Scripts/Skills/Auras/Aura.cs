using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : Effect
{
    public Effect effect;
    public Func<Attackable, bool> IsAffectedByAura;

    public Aura(float range, Effect effect) : base(range, 3600, 1, effect.IsValidTarget)
    {
        this.effect = effect;
    }

    public override void OnTick(Targetable target)
    {
        Collider[] enemiesCollider = Physics.OverlapSphere(target.transform.position, Range);

        foreach (Collider c in enemiesCollider)
        {
            if (IsValidTarget(target))
            {
                effect.ApplyOnTarget(target);
            }
        }

    }
}
