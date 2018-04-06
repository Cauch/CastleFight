using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : Attackable {
    // Private attributes
    const float BASE_GLOBAL_COOLDOWN = 0.2f;
    const float ANIMATOR_SPEED_MODIFIER = 0.02f;

    public Attackable EnemyCastle;
    public GameObject Corpse;
    public bool Corpseless = false;
    public GameObject Model;

    public float DefaultSpeed;
    public float DetectionRange;

    // Protected Attributes
    protected float _speedModifier;
    protected Skill[] _skills;
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
            if(_currentSkill == null)
            {
                _currentSkill = UseSkill();
            }

            if(_currentSkill != null)
            {
                _currentSkill = _currentSkill.Update();
                StopMoving();
            }
            else
            {
                Move(TargetingFunction.GetClosestEnemy(this));
            }

            RefreshCooldown();
        }
    }

    void RefreshCooldown()
    {
        foreach (ActiveSkill skill in _skills)
        {
            skill.Cooldown -= (Time.deltaTime * skill.SkillRefreshSpeed);
        }
    }

    //Public methods
    //Find root cause and change
    public void AdjustStart()
    {
        this.Allegiance = this.Creator.Allegiance;
        this.EnemyCastle = this.Allegiance ?
            GameObject.FindGameObjectWithTag("Castle0").GetComponent<Attackable>() :
            GameObject.FindGameObjectWithTag("Castle1").GetComponent<Attackable>();

        //Change color
        this.transform.GetChild(0).GetComponent<Renderer>().material.color = this.Allegiance ? Color.black : Color.white;

    }

    //Returns ActiveSkill to use, null if none
    public abstract ActiveSkill UseSkill();

    protected override void Die()
    {
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
        //ChangeModel();
    }

    private void OnDisable()
    {

    }

    private void ChangeModel()
    {
        if (_corpse)
        {
            Model.SetActive(true);
            _corpse.Model.SetActive(false);
        }
    }
}
