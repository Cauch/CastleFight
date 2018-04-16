using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIBuilderManager : MonoBehaviour {
    private Builder _builder;
    public Builder Builder {
        get { return NetworkHelper.IsOffline? _builder : NetworkHelper.Builder; }

        set
        {
            if (_builder != value)
            {
                // Could be avoided if Builders panels are memorized
                _builder = value;
                DestroyButtons();
                InstantiateButtons();
            }
        }
    }
    public GameObject ButtonTemplate;
    public GameObject BuildingPreviewDefault;

    private GameObject _buildingPreview;
    private Transform _buidlingsSubPanel;

    private Dictionary<IBuildingCost, Button> _buttons = new Dictionary<IBuildingCost, Button>();

    // Use this for initialization
    void Awake () {
        _buidlingsSubPanel = this.transform.GetChild(0);
    }

    private void InstantiateButtons()
    {
        foreach (GameObject building in Builder.Buildings)
        {
            IBuildingCost buildCost = building.GetComponent<IBuildingCost>();
            buildCost.Init();

            GameObject go = Instantiate(ButtonTemplate, _buidlingsSubPanel);
            Button button = go.GetComponent<Button>();
            button.onClick.AddListener(() => ClickBuild(building));

            _buttons[buildCost] = button;

            IEnumerable<IResource> cost = buildCost.GetResources();
            string costText = string.Join(Environment.NewLine, cost.Select((r) => r.ToString()).ToArray());

            button.GetComponentInChildren<Text>().text = building.name + Environment.NewLine + costText;
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

    private void Update()
    {
        if (Builder == null) return;

        foreach (KeyValuePair<IBuildingCost, Button> buildingButton in _buttons)
        {
            if(Builder.CanPayBuilding(buildingButton.Key))
            {
                buildingButton.Value.interactable = true;
            }
            else
            {
                buildingButton.Value.interactable = false;
            }
        }
    }

    void ClickBuild(GameObject building)
    {
        if (Builder.CanPayBuilding(building.GetComponent<IBuildingCost>()))
        {
            _buildingPreview = Instantiate(BuildingPreviewDefault);
            PreviewBuilding pb = _buildingPreview.GetComponent<PreviewBuilding>();
            building.GetComponent<Building>().Creator = Builder;
            pb.BuildingTemplate = building;
            pb.Builder = Builder;
            pb.Instantiate();
        }
        else
        {
            //Proc not enough money error
        }
    }
}
