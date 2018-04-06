using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : Effect
{
    public Effect effect;
    public Func<Attackable, bool> IsAffectedByAura;

    public Aura(float range, Effect effect) : base(range, 3600, 5, effect.isValidTarget)
    {
        this.effect = effect;
    }

    public override bool OnApply(Attackable target)
    {
        return true;
    }

    public override bool OnRemove(Attackable target)
    {
        return true;
    }

    public override bool OnTick(Attackable target)
    {
        Collider[] enemiesCollider = Physics.OverlapSphere(target.transform.position, Range);

        foreach (Collider c in enemiesCollider)
        {
            Attackable attackable = c.gameObject.GetComponent<Unit>();
            if (attackable != null && isValidTarget(attackable))
            {
                effect.Reset();
                attackable.AddEffect(effect);
            }
        }

        return true;
    }
}
