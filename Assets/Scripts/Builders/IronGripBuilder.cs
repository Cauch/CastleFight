using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronGripBuilder : Builder {
    public IResource Iron;
    public IResource Faith;

    public uint StartingIron = 100u;
    public uint BaseIronIncome = 10u;
    public uint StartingFaith = 0u;

    private IResource _baseIncomeIron;

    public float IncomeCooldown = 10;
    private float _time;

    new protected virtual void Start()
    {
        base.Start();

        _time = 0;

        Faith = new ResourceFaith(StartingFaith);
        Iron = new ResourceIron(StartingIron);
        _baseIncomeIron = new ResourceIron(BaseIronIncome);

        Resources = new List<IResource>
        {
            Iron,
            Faith,
        };
    }

    new protected virtual void Update()
    {
        _time += Time.deltaTime;

        if (_time > IncomeCooldown / GameSettings.IncomeSpeedModifier)
        {
            Iron.Add(_baseIncomeIron);
            _time = 0;
        }
    }
}
