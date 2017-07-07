using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIBuildingPanel : MonoBehaviour {
    public RectTransform parentPanel;
    public List<Button> buttons;
    public GameObject prefabButton;
    public GameObject thrashObject;

	// Use this for initialization
	void Start () {
        //Usualy received from player... perhaps
        buttons = new List<Button>();
        float offsetX = prefabButton.GetComponent<RectTransform>().rect.width;
        float offsetY = -prefabButton.GetComponent<RectTransform>().rect.height;

        Vector3 pos = new Vector3(parentPanel.rect.xMin + offsetX/2, parentPanel.rect.yMax + offsetY/2, 0);

        for (int i = 0; i < 5; i++)
        {
            GameObject goButton = (GameObject)Instantiate(prefabButton);
            goButton.transform.position = pos + new Vector3(i*offsetX, 0, 0);
            goButton.transform.SetParent(parentPanel, false);
            float f = i;
            goButton.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(f));
        }
    }

    void ButtonClicked(float buttonNo)
    {
        GameObject thrash = Instantiate(thrashObject);
        thrash.transform.position = new Vector3(buttonNo - 1, 0, 0);

        thrash.transform.SetParent(thrashObject.transform.parent, false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
