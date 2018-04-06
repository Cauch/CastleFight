using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject BuilderPanel;
    public GameObject BuildingPanel;
    public GameObject UnitPanel;

    public GameObject ResourcePanel;

    GameObject _currentPanel;
    GameObject _canvas;

    public Selectable DefaultSelectable;

	// Use this for initialization
	void Start () {
        _canvas = GameObject.FindGameObjectWithTag("Canvas");

        BuilderPanel = Instantiate(BuilderPanel, _canvas.transform);
        BuildingPanel = Instantiate(BuildingPanel, _canvas.transform);
        UnitPanel = Instantiate(UnitPanel, _canvas.transform);
        ResourcePanel = Instantiate(ResourcePanel, _canvas.transform);

        _currentPanel = BuildingPanel;
        BuildingPanel.SetActive(false);
        UnitPanel.SetActive(false);
    }

    public void ReplacePanel(GameObject panel)
    {
        if(_currentPanel == panel)
        {
            return;
        }
        this._currentPanel.SetActive(false);
        this._currentPanel = panel;
        this._currentPanel.SetActive(true);
    }

    public void ReplacePanel(Selectable selectable)
    {
        switch(selectable.PanelType)
        {
            case PanelType.BUILDER:
                Builder builder = selectable.GetComponent<Builder>();
                UIBuilderManager builderManager = BuilderPanel.GetComponent<UIBuilderManager>();
                UIResourceManager resourceManager = ResourcePanel.GetComponent<UIResourceManager>();

                builderManager.Builder = builder;
                resourceManager.Builder = builder;
                ReplacePanel(BuilderPanel);
                break;
            case PanelType.BUILDING:
                Building building = selectable.GetComponent<Building>();
                UIBuildingManager buildingManager = BuildingPanel.GetComponent<UIBuildingManager>();

                buildingManager.Building = building;
                ReplacePanel(BuildingPanel);
                break;
            case PanelType.UNIT:
                Unit unit = selectable.GetComponent<Unit>();
                UIUnitManager unitManager = UnitPanel.GetComponent<UIUnitManager>();

                unitManager.Unit = unit;
                ReplacePanel(UnitPanel);
                break;
            case PanelType.CORPSE:
                break;
        }
    }

    public void ReplacePanelDefault()
    {
        ReplacePanel(DefaultSelectable);
    }
}
