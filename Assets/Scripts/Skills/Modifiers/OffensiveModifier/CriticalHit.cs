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
        float crit = Random.Range(0f, 1f);

        if (crit < chance)
        {
            attack.dmg *= multiplicator;
        }

        return attack;
    }

    public float ModifyDefense(float armor)
    {
        return armor;
    }
}
