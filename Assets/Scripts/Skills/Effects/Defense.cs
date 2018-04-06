using System;

public class Defense : Effect {
    float physicalArmorMultiplicator;
    float magicalArmorMultiplicator = 1;

    public Defense(float physicalArmorMultiplicator, Func<Attackable, bool> IsValidTarget) : base(0, 1, 1, IsValidTarget)
    {
        this.physicalArmorMultiplicator = physicalArmorMultiplicator;
    }

    public override void OnApply(Targetable target)
    {
        Attackable attackable = target as Attackable;
        attackable.ArmorMods += physicalArmorMultiplicator * attackable.BaseArmor;
    }

    public override void OnRemove(Targetable target)
    {
        Attackable attackable = target as Attackable;
        attackable.ArmorMods -= physicalArmorMultiplicator * attackable.BaseArmor;
    }
}
