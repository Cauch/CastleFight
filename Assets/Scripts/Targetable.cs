using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Targetable : Selectable {
    protected List<Effect> _effects;

    new protected virtual void Start()
    {
        base.Start();
        _effects = new List<Effect>();
    }

    protected void Update()
    {
        RefreshEffects();
        TriggerEffects();
    }

    void TriggerEffects()
    {
        //Tick time should be in this class, for cacheline reason ?
        for (int i = 0; i < _effects.Count; i++)
        {
            if (_effects[i].TickTime < 0)
            {
                _effects[i].OnTick(this);
                _effects[i].TickTime = 1;
            }
        }
    }

    void RefreshEffects()
    {
        List<Effect> effectToRemove = new List<Effect>();
        foreach (Effect effect in _effects)
        {
            effect.TickTime -= effect.TickSpeed * Time.deltaTime;
            effect.AppliedTime += Time.deltaTime;
            if (effect.Duration < effect.AppliedTime)
            {
                effect.OnRemove(this);
                effectToRemove.Add(effect);
            }
        }

        foreach (Effect effect in effectToRemove)
        {
            _effects.Remove(effect);
        }
    }

    public void AddEffect(Effect effect)
    {
        effect.OnApply(this);
        _effects.Add(effect); 
    }

    public bool AlreadyAffected(Effect effect, int stacks = 1)
    {
        return _effects.Where(t => t.GetType() == effect.GetType()).Count() >= stacks;
    }

}
