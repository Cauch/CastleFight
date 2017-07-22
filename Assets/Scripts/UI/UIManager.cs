using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    GameObject currentPanel;
    public GameObject defaultPanel;
    GameObject canvas;
	// Use this for initialization
	void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        this.currentPanel = Instantiate(defaultPanel);
        this.currentPanel.transform.SetParent(canvas.transform);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ReplacePanel(GameObject panel)
    {   
        this.currentPanel.SetActive(false);
        this.currentPanel = (panel);
        this.currentPanel.SetActive(true);
        this.currentPanel.transform.SetParent(canvas.transform); // Could be placed at the instantiation of the panels
        
    }
}
