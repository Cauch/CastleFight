using System;

public class Defense : Effect {
    float physicalArmorMultiplicator;
    float magicalArmorMultiplicator = 1;

    public Defense(float physicalArmorMultiplicator, Func<Attackable, bool> IsValidTarget) : base(0, 1, 1, IsValidTarget)
    {
        this.physicalArmorMultiplicator = physicalArmorMultiplicator;
    }

    public override bool OnApply(Attackable target)
    {
        target.ArmorMods += physicalArmorMultiplicator * target.BaseArmor;
        return true;
    }

    public override bool OnRemove(Attackable target)
    {
        target.ArmorMods -= physicalArmorMultiplicator * target.BaseArmor;
        return true;
    }

    public override bool OnTick(Attackable target)
    {
        return true;
    }
}
