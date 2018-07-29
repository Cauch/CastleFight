using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : Attackable {
    // Private attributes
    const float BASE_GLOBAL_COOLDOWN = 0.2f;
    const float ANIMATOR_SPEED_MODIFIER = 0.02f;

    public bool Corpseless = false;
    public GameObject Corpse;
    public GameObject Model;

	protected Move _move;
	protected bool _headingForCastle = false;

    public float DefaultSpeed;
    public float Speed;
    public float DetectionRange;

    public Skill[] Skills;

    // Protected Attributes
    protected float _speedModifier;
    protected float _generalColdown;

    // Abstract Methods
    protected abstract void Move(Targetable target);
    protected abstract void StopMoving();

    protected Corpse _corpse;

    protected ActiveSkill _currentSkill;

    private void Awake()
    {
        PanelType = PanelType.UNIT;
        if (!Corpseless)
        {
            _corpse = GetComponent<Corpse>();
            _corpse.enabled = false;
        }
    }
    // Private and protected methods
    new protected void Start()
    {
        base.Start();
    }

    protected virtual new void Update()
    {
        base.Update();
        if (IsActive)
        {
			// If you have a skill try using it
			if (_currentSkill != null)
			{
				_currentSkill = _currentSkill.Update();
			}

            // If you are doing nothing, chose a skill
            if(_currentSkill == null || _headingForCastle)
            {
                _currentSkill = UseSkill();
				_headingForCastle = false;
			}

			// TODO cauch remove when each skill has its own implementation in use skill
			if (_currentSkill == null || _headingForCastle)
			{
				Targetable target = TargetingFunction.GetClosestTarget(this, null, (Targetable t) => TargetingFunction.IsEnemyUnit(this, t), this.DetectionRange);
				if (target)
				{
					_move.ApplyOnTarget(target);
					_currentSkill = _move;
				}
			}				

			if (_currentSkill == null)
			{
				_move.ApplyOnTarget(CastleHelper.GetCastle(!Allegiance));
				_headingForCastle = true;
				_currentSkill = _move;
			}

			// TODO cauch find somewhere better to do that
			if (Hp <= 0)
			{
				_move.Break();
			}

            RefreshCooldown();
        }
    }

    void RefreshCooldown()
    {
        foreach (ActiveSkill skill in Skills)
        {
            skill.Cooldown -= (Time.deltaTime * skill.BaseSkillRefreshSpeed);
        }
    }

    //Public methods
    //Find root cause and change
    public virtual void AdjustStart()
    {
        IsActive = true;
        //Change color
        this.transform.GetChild(0).GetComponent<Renderer>().material.color = this.Allegiance ? Color.black : Color.white;

    }

    //Returns ActiveSkill to use, null if none
    public abstract ActiveSkill UseSkill();

    protected override void Die()
    {
        foreach(Effect effect in _effects)
        {
            effect.OnDeath(this);
        }

        if (_currentSkill != null)
        {
            _currentSkill.Break();
            _currentSkill = null;
        }
        if(!Corpseless)
        {
            _corpse.enabled = true;
            this.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.blue;
            this.enabled = false;
        }
        else
        {
            base.Die();
        }
    }

    private void OnEnable()
    {
		_move.SetEnable(true);
        //ChangeModel();
    }

    private void OnDisable()
    {
		_move.SetEnable(false);
    }

    private void ChangeModel()
    {
        if (_corpse)
        {
            Model.SetActive(true);
            _corpse.Model.SetActive(false);
        }
    }

    public abstract void SetSpeed(float speed);
    public virtual Targetable MoveTarget()
    {
        return TargetingFunction.GetClosestTarget(this, CastleHelper.GetCastle(!Allegiance), (Targetable Targetable) => TargetingFunction.IsEnemy(this, Targetable), DetectionRange * DetectionRange);
    }
}
