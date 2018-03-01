using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingManager : UIItemManager
{
    public Text HpText;
    public Text ArmorText;

    private Building _building;
    public Building Building
    {
        get { return _building; }

        set
        {
            if (_building != value)
            {
                // Could be avoided if Builders panels are memorized
                _building = value;
                DestroyButtons();

                InstantiateButtons();
            }
        }
    }

    public GameObject ButtonTemplate;
    public GameObject TextTemplate;
    private Transform _progressBar;

    private Transform _upgradesSubPanel;
    private Dictionary<IBuildingCost, Button> _buttons = new Dictionary<IBuildingCost, Button>();

    // Use this for initialization
    void Awake()
    {
        _upgradesSubPanel = this.transform.GetChild(1);
        _progressBar = this.transform.GetChild(2);
    }

    private void InstantiateButtons()
    {
        foreach (GameObject upgrade in Building.Upgrades)
        {
            IBuildingCost buildCost = upgrade.GetComponent<IBuildingCost>();
            buildCost.Init();

            GameObject go = Instantiate(ButtonTemplate, _upgradesSubPanel);
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => ClickUpgrade(upgrade));

            _buttons[buildCost] = button;

            IEnumerable<IResource> cost = buildCost.GetResources();
            string costText = string.Join(Environment.NewLine, cost.Select((r) => r.ToString()).ToArray());

            button.GetComponentInChildren<Text>().text = upgrade.name + Environment.NewLine + costText;
        }
    }

    private void DestroyButtons()
    {
        foreach (KeyValuePair<IBuildingCost, Button> kv in _buttons)
        {
            Destroy(kv.Value.gameObject);
        }

        _buttons.Clear();
    }

    void ClickUpgrade(GameObject upgrade)
    {
        if (Building.Creator.CanPayBuilding(upgrade.GetComponent<IBuildingCost>()))
        {
            GameObject previousBuilding = this.Building.gameObject;
            Building buidling = Instantiate(upgrade, previousBuilding.transform.position, previousBuilding.transform.rotation, previousBuilding.transform.parent).GetComponent<Building>();
            buidling.Creator = this.Building.Creator;
            buidling.AdjustStart();

            Destroy(previousBuilding);
        }
        else
        {
            //Proc not enough money error
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Building == null) return;
        
        HpText.text = "HP: " + Building.Hp.ToString() + "/" + Building.MaxHp.ToString();
        ArmorText.text = "Armor: " + Building.Armor.ToString();
        Image image = _progressBar.GetComponent<Image>();
        image.fillAmount = Building.Loading / Building.MaxTime;
    }
}