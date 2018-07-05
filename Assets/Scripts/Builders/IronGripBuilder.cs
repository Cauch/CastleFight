using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronGripBuilder : Builder {
    public Resource Iron;
    public Resource Faith;

    public uint StartingIron = 100u;
    public uint BaseIronIncome = 10u;
    public uint StartingFaith = 0u;

    private Resource _baseIncomeIron;

    public float IncomeCooldown = 10;
    private float _time;

    new protected virtual void Start()
    {
        base.Start();

        _time = 0;

        Faith = new ResourceFaith(StartingFaith);
        Iron = new ResourceIron(StartingIron);
        _baseIncomeIron = new ResourceIron(BaseIronIncome);

        Resources = new List<Resource>
        {
            Iron,
            Faith,
        };
    }

    protected virtual void Update()
    {
        _time += Time.deltaTime;

        if (_time > IncomeCooldown / GameSettings.IncomeSpeedModifier)
        {
            Iron.Add(_baseIncomeIron);
            _time = 0;
        }
    }
}
