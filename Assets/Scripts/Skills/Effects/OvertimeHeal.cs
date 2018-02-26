using System;

public class OvertimeHeal : Effect
{
    float heal;
    public OvertimeHeal(float heal, Func<Attackable, bool> IsValidTarget) : base(0, 1, 1, IsValidTarget)
    {
        this.heal = heal;
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
        target.Hp = Math.Min(target.Hp + heal, target.MaxHp);
        return true;
    }
}