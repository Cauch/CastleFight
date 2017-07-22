using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitManager : MonoBehaviour
{
    public Text hpText;
    public Text armorText;
    public Text attackText;
    public Text attackSpeedText;
    public Unit unit;

    // Use this for initialization
    void Start()
    {
        //unit = unitObject.GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {

        if(unit != null)
        {
            hpText.text = unit.hp.ToString() + "/" + unit.maxHp.ToString();
            armorText.text = unit.armor.ToString();
        }

    }
}