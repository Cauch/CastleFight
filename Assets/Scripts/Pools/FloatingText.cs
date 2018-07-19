using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour, IPooledObject
{
    public TextMesh Text;
    public float LifeTime = 2f;

    private float _timer;

    public void Update()
    {
        if(Time.time - _timer > LifeTime)
        {
            // TODO cauch this should be taken in charge by the pooler
            this.gameObject.SetActive(false);
        }
    }

    public void OnSpawn(object[] parameters)
    {
        if(parameters != null)
        {
            Debug.Log("Error: Floating text do not require parameters such as " + parameters[0]);
        }

        _timer = Time.time;
        this.GetComponent<Animator>().Play("FloatingText", -1, 0f);
    }

    public void SetText(string message, Color color)
    {
        Text.text = message;
        Text.color = color;
    }
}
