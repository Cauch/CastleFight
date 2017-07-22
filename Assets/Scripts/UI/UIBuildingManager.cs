using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingManager : UIItemManager
{
    public Text hpText;
    public Text armorText;  
    public Building building;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (building != null)
        {
            hpText.text = building.hp.ToString() + "/" + building.maxHp.ToString();
            armorText.text = building.armor.ToString();
        }

    }
}