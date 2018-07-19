using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIBuildingManager : MonoBehaviour
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

    private Image _image;

    private Transform _upgradesSubPanel;
    private Dictionary<IBuildingCost, Button> _buttons = new Dictionary<IBuildingCost, Button>();
    private UIManager _uiManager;

    // Use this for initialization
    void Awake()
    {
        _upgradesSubPanel = this.transform.GetChild(1);
        _progressBar = this.transform.GetChild(2);
        _image = _progressBar.GetComponent<Image>();
    }

    // Use this for initialization
    void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
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

            IEnumerable<Resource> cost = buildCost.GetResources();
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
        Builder Creator = NetworkHelper.IsOffline ? BuilderHelper.GetBuilderById(Building.CreatorId) : NetworkHelper.Builder;
        if (!Creator.CanPayBuilding(upgrade.GetComponent<IBuildingCost>()))
        {
            // Proc not enough money message
        }
        else
        {
            Creator.PayBuilding(upgrade.GetComponent<IBuildingCost>());
            GameObject previousBuilding = this.Building.gameObject;
            InstantiateBuilding(upgrade, Creator, previousBuilding);
            Destroy(previousBuilding);
        }
    }

    void InstantiateBuilding(GameObject building, Builder creator, GameObject previousBuilding)
    {
        if(NetworkHelper.IsOffline)
        {
            Building buidling = Instantiate(building, previousBuilding.transform.position, previousBuilding.transform.rotation, previousBuilding.transform.parent).GetComponent<Building>();
            buidling.CreatorId = creator.Id;
            buidling.Allegiance = creator.Allegiance;
        }
        else
        {
            NetworkHelper.InstantiateBuilding(building, previousBuilding.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Building == null)
        {
            return;
        }

        if(Building.Hp <= 0)
        {
            _uiManager.ReplacePanelDefault();
        }

        foreach (KeyValuePair<IBuildingCost, Button> buildingButton in _buttons)
        {
            Builder builder = NetworkHelper.IsOffline ? BuilderHelper.GetBuilderById(Building.CreatorId) : NetworkHelper.Builder;
            if (builder.CanPayBuilding(buildingButton.Key))
            {
                buildingButton.Value.interactable = true;
            }
            else
            {
                buildingButton.Value.interactable = false;
            }
        }

        HpText.text = "HP: " + Building.Hp.ToString() + "/" + Building.MaxHp.ToString();
        ArmorText.text = "Armor: " + Building.Armor.ToString();
        _image.fillAmount = Building.Loading / Building.MaxTime;
    }
}