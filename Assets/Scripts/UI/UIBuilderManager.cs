using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuilderManager : MonoBehaviour {
    public Builder builder;
    public GameObject buttonTemplate;
    public GameObject buildingPreviewDefault;
    GameObject buildingPreview;

	// Use this for initialization
	void Start () {
		foreach(GameObject building in builder.buildings)
        {
            GameObject button = Instantiate(buttonTemplate);
            button.transform.SetParent(this.transform);
            button.GetComponent<Button>().onClick.AddListener(() => ClickBuild(building));
            button.GetComponentInChildren<Text>().text = building.name;
        }   
	}

    void ClickBuild(GameObject building)
    {
        if (builder.EnoughMoney(building.GetComponent<Building>().cost))
        {
            buildingPreview = Instantiate(buildingPreviewDefault);
            PreviewBuilding pb = buildingPreview.GetComponent<PreviewBuilding>();
            pb.buildingTemplate = building;
            pb.builder = builder;
            pb.Instantiate();
        } else
        {
            //Proc not enough money error
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
