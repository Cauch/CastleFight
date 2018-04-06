public class Block : IDefensiveModifier
{
    float _reduction;
    float _chance;

    public Block(float reduction, float chance)
    {
        this._reduction = reduction;
        this._chance = chance;
    }

    public Attack ModifyAttack(Attack attack)
    {
        float crit = UnityEngine.Random.Range(0f, 1f);

        if (crit < _chance)
        {
            attack.Damage = System.Math.Max(attack.Damage - _reduction, 0);
        }
        return attack;
    }

    public float ModifyDefense(float armor)
    {
        return armor;
    }
}