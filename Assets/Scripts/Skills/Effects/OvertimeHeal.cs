using System;

public class OvertimeHeal : Effect
{
    float heal;
    public OvertimeHeal(float heal, Func<Attackable, bool> IsValidTarget) : base(0, 1, 1, IsValidTarget)
    {
        this.heal = heal;
    }

    public override void OnTick(Targetable target)
    {
        Attackable attackable = target as Attackable;
        attackable.AddHp(heal);
    }
}