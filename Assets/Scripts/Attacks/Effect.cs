using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect {
    public float cooldown;
    public float duration;

    public abstract bool OnApply(Attackable target);
    public abstract bool OnTick(Attackable target);
    public abstract bool OnRemove(Attackable target);
}
