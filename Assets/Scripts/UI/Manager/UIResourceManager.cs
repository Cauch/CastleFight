using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResourceManager : MonoBehaviour {
    private Builder _builder;
    public Builder Builder {
        get { return _builder; }

        set
        {
            if (_builder != value)
            {
                // Could be avoided if Builders panels are memorized
                _builder = value;
                DestroyTexts();
                InstantiateTexts();
            }
        }
    }

    public GameObject TextTemplate;

    private Dictionary<Resource, Text> _texts = new Dictionary<Resource,Text>();

    private void Update()
    {
        if (Builder == null) return;

        foreach (Resource resource in Builder.Resources)
        {
            _texts[resource].text = resource.ToString();
        }
    }

    private void InstantiateTexts()
    {
        foreach (Resource resource in Builder.Resources)
        {
            GameObject text = Instantiate(TextTemplate, this.transform);
            _texts.Add(resource, text.GetComponent<Text>());
        }
    }

    private void DestroyTexts()
    {
        foreach (KeyValuePair<Resource, Text> kv in _texts)
        {
            Destroy(kv.Value.gameObject);
        }

        _texts.Clear();
    }
}
