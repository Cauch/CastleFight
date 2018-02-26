using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    GameObject _currentPanel;
    public GameObject DefaultPanel;
    GameObject _canvas;
	// Use this for initialization
	void Start () {
        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        this._currentPanel = Instantiate(DefaultPanel);
        this._currentPanel.transform.SetParent(_canvas.transform);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ReplacePanel(GameObject panel)
    {
        Transform test = panel.transform;
        if(this._currentPanel != null)
        {
            this._currentPanel.SetActive(false);
        }
        this._currentPanel = (panel);
        this._currentPanel.SetActive(true);
        this._currentPanel.transform.SetParent(_canvas.transform,false);// Could be placed at the instantiation of the panels
        test = panel.transform;
    }
}
