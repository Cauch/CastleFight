using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class TargetingFunction {
    public static Func<Targetable, bool> ToTargetableFunc(Func<Attackable, bool> func)
    {
        return (Targetable t) => IsAttackable(t) && func(t as Attackable);
    }

    public static IEnumerable<Attackable> DetectSurroundings(Unit attacker, Func<Attackable, bool> isValidTarget)
    {
        return DetectSurroundings(attacker, ToTargetableFunc(isValidTarget)).Cast<Attackable>();
    }

    public static List<Targetable> DetectSurroundings(Unit attacker, Func<Targetable, bool> isValidTarget)
    {
        Collider[] colliders = Physics.OverlapSphere(attacker.transform.position, attacker.DetectionRange);
        List<Targetable> targets = new List<Targetable> { };

        foreach (Collider c in colliders)
        {
            Targetable target = c.gameObject.GetComponents<Targetable>().FirstOrDefault(t => t.enabled == true);
            if (isValidTarget(target))
            {
                targets.Add(target);
            }
        }

        return targets;
    }

    public static ActiveSkill UseAttack(Unit attacker, Attack attack)
    {
        bool atLeastOneTargetInRange = false;

        IEnumerable<Attackable> targets = TargetingFunction.DetectSurroundings(attacker, attack.isValidTarget).Cast<Attackable>();

        foreach (Attackable target in targets)
        {
            bool targetInRange = attack.IsUsable(attacker, target);
            atLeastOneTargetInRange = atLeastOneTargetInRange || targetInRange;
            if (targetInRange)
            {
                if (attack.Cooldown <= 0)
                {
                    attack.ApplyOnTarget(target);
                    attack.Cooldown = 1.0f;
                    return attack;
                }
            }
        }

        return null;
    }

    public static Targetable GetClosestEnemy(Unit attacker)
    {
        Targetable target = attacker.EnemyCastle;

        float closestDistanceSqr = attacker.DetectionRange;

        foreach (Targetable potentialTarget in DetectSurroundings(attacker, (Targetable Targetable) => TargetingFunction.IsEnemy(attacker, Targetable)))
        {
            Vector3 directionToTarget = potentialTarget.transform.position - attacker.transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr && potentialTarget.gameObject != attacker)
            {
                closestDistanceSqr = dSqrToTarget;
                target = potentialTarget;
            }
        }

        return target;
    }

    public static bool IsEnemy(Targetable attacker, Targetable target)
    {
        if (attacker == null || target == null) return false;
        return attacker.Allegiance != target.Allegiance && target.IsActive && IsAttackable(target);
    }
    public static bool IsAlly(Targetable attacker, Targetable target)
    {
        if (attacker == null || target == null) return false;
        return attacker.Allegiance == target.Allegiance && IsAttackable(target);
    }
    public static bool IsUnit(Targetable Targetable)
    {
        return Targetable is Unit;
    }

    public static bool IsInRange(Vector3 v1, Vector3 v2, float range)
    {
        return (v1 - v2).sqrMagnitude <= Mathf.Pow(range, 2);
    }

    public static bool IsInRangeorMelee(MonoBehaviour m1, MonoBehaviour m2, float range)
    {
        if(m1 && m2)
        {
            return IsInRange(m1.transform.position, m2.transform.position, range) || IsMelee(m1, m2);
        }
        return false;
       
    }

    public static bool IsMelee(MonoBehaviour m1, MonoBehaviour m2)
    {
        Bounds aBounds = m1.GetComponent<Collider>().bounds;
        Bounds tBounds = m2.GetComponent<Collider>().bounds;

        return tBounds.Intersects(aBounds);
    }

    public static bool IsCorpse(Targetable target)
    {
        return target is Corpse && (target as Corpse).enabled == true;
    }

    public static bool IsAttackable(Targetable target)
    {
        return target is Attackable && (target as Attackable).enabled == true;
    }

}
