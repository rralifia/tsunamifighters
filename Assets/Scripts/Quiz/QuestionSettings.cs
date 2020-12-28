using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionSettings : MonoBehaviour
{
    //public GameObject numberLabel;
    void Start()
    {
        PlayerPrefs.SetInt("QuestNumber", 1);
    }

    // Update is called once per frame
    void Update()
    {
        // get integer of text component in GUI
        // convert into string because .text must be string
        GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("QuestNumber").ToString();
    }
}
