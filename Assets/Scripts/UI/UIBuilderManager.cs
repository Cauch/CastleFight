using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIBuilderManager : MonoBehaviour {
    public Builder Builder;
    public GameObject ButtonTemplate;
    public GameObject TextTemplate;
    public GameObject BuildingPreviewDefault;

    private GameObject _buildingPreview;
    private Transform _buidlingsSubPanel;
    private Transform _resourcesSubPanel;

    private Dictionary<IResource, Text> _texts = new Dictionary<IResource,Text>();
    private Dictionary<IBuildingCost, Button> _buttons = new Dictionary<IBuildingCost, Button>();

    // Use this for initialization
    void Start () {
        _buidlingsSubPanel = this.transform.GetChild(0);
        _resourcesSubPanel = this.transform.GetChild(1);

        //Instantiate buttons
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
        //Instantiate resource texts for Builder
        foreach (IResource resource in Builder.Resources)
        {
            GameObject text = Instantiate(TextTemplate, _resourcesSubPanel);
            _texts.Add(resource, text.GetComponent<Text>());
        }

    }

    private void FixedUpdate()
    {
        foreach (IResource resource in Builder.Resources)
        {
            _texts[resource].text = resource.ToString();
        }

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
            pb.BuildingTemplate = building;
            pb.Builder = Builder;
            pb.Instantiate();
        }
        else
        {
            //Proc not enough money error
        }
    }

	// Update is called once per frame
	void Update ()
    {
		
	}
}
