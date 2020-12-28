using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string key;
    //public GameObject textbox;
    public TextMeshProUGUI text;

    // Use this for initialization
    void Start()
    {
        // textValue =

        text.text = LocalizationManager.instance.GetLocalizedValue(key);
        // Debug.Log("Localized Text: " + text);
    }

}