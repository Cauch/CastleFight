using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitManager : MonoBehaviour
{
    public Text HpText;
    public Text ArmorText;
    public Text DescriptionText;
    public Unit Unit;

    private UIManager _uiManager;
    // Use this for initialization
    void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Unit == null) return;
        if(Unit.Hp <= 0)
        {
            _uiManager.ReplacePanelDefault();
        }
        HpText.text = "HP: " + Unit.Hp.ToString() + "/" + Unit.MaxHp.ToString();
        ArmorText.text = "Armor: " + Unit.Armor.ToString();
        DescriptionText.text = Unit.ToString();
    }
}