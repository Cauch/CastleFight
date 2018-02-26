using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TargetingFunction {
    public static List<Attackable> DetectSurroundings(Unit attacker, Func<Attackable, bool> isValidTarget)
    {
        Collider[] colliders = Physics.OverlapSphere(attacker.transform.position, attacker.DetectionRange);
        List<Attackable> targets = new List<Attackable> { };

        foreach (Collider c in colliders)
        {
            Attackable target = c.gameObject.GetComponent<Attackable>();
            if (isValidTarget(target))
            {
                targets.Add(target);
            }
        }

        return targets;
    }

    public static bool UseAttack(Unit attacker, Attack attack)
    {
        bool atLeastOneTargetInRange = false;

        List<Attackable> targets = TargetingFunction.DetectSurroundings(attacker, attack.isValidTarget);

        foreach (Attackable target in targets)
        {
            bool targetInRange = attack.IsUsable(attacker, target);
            atLeastOneTargetInRange = atLeastOneTargetInRange || targetInRange;
            if (targetInRange)
            {
                if (attack.cooldown <= 0)
                {
                    attack.ApplyOnTarget(target);
                    attack.cooldown = 1.0f;
                    return true;
                }
            }
        }

        return atLeastOneTargetInRange;
    }

    public static Attackable GetClosestEnemy(Unit attacker)
    {
        Attackable target = attacker.EnemyCastle;

        float closestDistanceSqr = attacker.DetectionRange;

        foreach (Attackable potentialTarget in DetectSurroundings(attacker, (Attackable attackable) => TargetingFunction.IsEnemy(attacker, attackable)))
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

    public static bool IsEnemy(Attackable attacker, Attackable target)
    {
        if (attacker == null || target == null) return false;
        return attacker.allegiance != target.allegiance && target.isActive;
    }
    public static bool IsAlly(Attackable attacker, Attackable target)
    {
        if (attacker == null || target == null) return false;
        return attacker.allegiance == target.allegiance;
    }
}
