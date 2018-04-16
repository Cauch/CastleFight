using UnityEngine;

public class CriticalHit : IOffensiveModifier
{
    float multiplicator;
    float chance;

    public CriticalHit(float chance, float multiplicator)
    {
        this.chance = chance;
        this.multiplicator = multiplicator;
    }

    public Attack ModifyAttack(Attack attack)
    {
        float crit = ((float)RandomHelper.Random.Next(0, 100)) / 100f;

        if (crit < chance)
        {
            attack.Damage *= multiplicator;
        }

        return attack;
    }

    public float ModifyDefense(float armor)
    {
        return armor;
    }
}
