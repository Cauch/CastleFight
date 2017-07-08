using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    GameObject currentPanel;
    public GameObject defaultPanel;
    public GameObject canvas;
	// Use this for initialization
	void Start () {
        this.currentPanel = Instantiate(defaultPanel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReplacePanel(GameObject panel)
    {
        if (currentPanel.Equals(panel)) return;
        Destroy(currentPanel);  
        this.currentPanel = Instantiate(panel);
        this.currentPanel.transform.SetParent(canvas.transform);
    }
}
