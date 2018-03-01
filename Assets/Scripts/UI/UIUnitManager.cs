using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitManager : MonoBehaviour
{
    public Text HpText;
    public Text ArmorText;
    public Unit Unit;
    // Use this for initialization
    void Start()
    {
        //unit = unitObject.GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Unit == null) return;

        HpText.text = "HP: " + Unit.Hp.ToString() + "/" + Unit.MaxHp.ToString();
        ArmorText.text = "Armor: " + Unit.Armor.ToString();
        
    }
}