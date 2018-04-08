using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Harvest : ChannelSkill
{
    public bool IsFull = false;

    private uint _maxBiomass;
    public uint _currentBiomass;
    private float _decompositionPerBiomass;

    private Builder _builder;

    public Harvest(uint maxBiomass, float decompositionRate, Attackable caster, float tickSpeed, Func<Targetable, bool> isValidTarget) : base(0, 10, 100, caster, tickSpeed, isValidTarget)
    {
        _maxBiomass = maxBiomass;
        _builder = caster.Creator;
        _decompositionPerBiomass = decompositionRate;
    }

    protected override void Tick(Targetable target)
    {
        if (_builder is HalfOnesBuilder)
        {
            Corpse corpse = target as Corpse;

            corpse.DecompositionTime -= _decompositionPerBiomass;
            _currentBiomass = Math.Min(_currentBiomass + 1u, _maxBiomass);

            if(_currentBiomass >= _maxBiomass)
            {
                IsFull = true;
                Break();
            }
        }
    }

    public void Deliver()
    {
        _builder.Resources.Where(r => r is ResourceBiomass).First().Add(new ResourceBiomass(_currentBiomass));
        _currentBiomass = 0;
        IsFull = false;
    }

    protected override void Complete() { }
}
