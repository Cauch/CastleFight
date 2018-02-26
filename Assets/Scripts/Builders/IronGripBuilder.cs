using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronGripBuilder : Builder {
    public IResource Iron;
    public IResource Faith;

    private IResource _baseIncomeIron;

    protected float _cooldown;
    private float _time;

    new protected virtual void Start()
    {
        base.Start();

        _time = 0;
        _cooldown = 10;

        Faith = new ResourceFaith(0u);
        Iron = new ResourceIron(100u);
        _baseIncomeIron = new ResourceIron(10u);

        Resources = new List<IResource>
        {
            Iron,
            Faith,
        };
    }

    new protected virtual void Update()
    {
        base.Update();

        _time += Time.deltaTime;

        if (_time > _cooldown / GameSettings.IncomeSpeedModifier)
        {
            Iron.Add(_baseIncomeIron);
            _time = 0;
        }
    }
}
