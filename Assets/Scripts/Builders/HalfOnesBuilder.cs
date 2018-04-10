using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfOnesBuilder : Builder {
    public IResource BioMass;

    public uint StartingBiomass = 100u;
    public uint BaseBiomassIncome = 10u;

    private IResource _baseIncomeBiomass;

    public float IncomeCooldown = 10;
    private float _time;

    new protected virtual void Start()
    {
        base.Start();

        _time = 0;

        BioMass = new ResourceBiomass(StartingBiomass);
        _baseIncomeBiomass = new ResourceIron(BaseBiomassIncome);

        Resources = new List<IResource>
        {
            BioMass
        };
    }

    new protected virtual void Update()
    {
        _time += Time.deltaTime;

        if (_time > IncomeCooldown / GameSettings.IncomeSpeedModifier)
        {
            BioMass.Add(_baseIncomeBiomass);
            _time = 0;
        }
    }
}
