using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Targetable : Selectable {
    protected List<Effect> effects;

    new protected virtual void Start()
    {
        base.Start();
        effects = new List<Effect>();
    }

    protected void Update()
    {
        RefreshEffects();
        TriggerEffects();
    }

    void TriggerEffects()
    {
        //Tick time should be in this class, for cacheline reason ?
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].tickTime < 0)
            {
                effects[i].OnTick(this);
                effects[i].tickTime = 1;
            }
        }
    }

    void RefreshEffects()
    {
        List<Effect> effectToRemove = new List<Effect>();
        foreach (Effect effect in effects)
        {
            effect.tickTime -= effect.tickSpeed * Time.deltaTime;
            effect.appliedTime += Time.deltaTime;
            if (effect.duration < effect.appliedTime)
            {
                effect.OnRemove(this);
                effectToRemove.Add(effect);
            }
        }

        foreach (Effect effect in effectToRemove)
        {
            effects.Remove(effect);
        }
    }

    public void AddEffect(Effect effect)
    {
        if (AlreadyAffected(effect))
        {

        }
        else
        {
            effect.OnApply(this);
            effects.Add(effect);
        }
    }

    public bool AlreadyAffected(Effect effect)
    {
        return effects.Where(t => t.GetType() == effect.GetType()).Any();
    }
}
