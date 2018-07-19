using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{
    public float FlyingSpeed = 10f;

    private Attack.AttackArmorContact _contact;

    public void Update()
    {
        if(!_contact.Attackable)
        {
            this.gameObject.SetActive(false);
        }

        // TODO cauch think of a way to set trajectory path (lob, vs javelin throw?)
        Vector3 direction = _contact.Attackable.transform.position - this.transform.position;

        this.transform.position += FlyingSpeed * Time.deltaTime * direction.normalized;
    }

    public void OnSpawn(object[] parameters)
    {
        if (parameters.Length < 0)
        {
            Debug.Log("Error: Projectile object spawns with not enough parametersL " + parameters.Length);
        }

        if (parameters[0] is Attack.AttackArmorContact == false)
        {
            Debug.Log("Error: Projectile does not contain Attackable target as first parameter " + parameters[0]);
        }

        if (parameters.Length > 1)
        {
            Debug.Log("Error: Projectile object spawns with too many parameters: " + parameters.Length);
        }

        _contact = (Attack.AttackArmorContact)parameters[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == _contact.Attackable.GetComponent<Collider>())
        {
            float totalDmg = _contact.Attack.Damage * (1 - _contact.Armor / 100f);
            _contact.Attackable.AddHp(-totalDmg);

            this.gameObject.SetActive(false);
        }
    }
}
