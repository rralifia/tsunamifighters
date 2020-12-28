using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizScoreSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("QuizScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        // get integer of text component in GUI
        // convert into string because .text must be string
        GetComponent<TextMeshProUGUI> ().text = PlayerPrefs.GetInt("QuizScore").ToString();
    }
}