using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAnswers : MonoBehaviour
{
    public string choice;
    int currentquest;

    // Start is called before the first frame update
    void Start()
    {
        currentquest = PlayerPrefs.GetInt("QuestNumber");
        //Debug.Log("Question number: " + currentquest);
    }

    // Update is called once per frame
    public void SetAnswer()
    {
        Opening.quizAnswers[currentquest] = choice;
        Debug.Log("Quiz Answers number " + currentquest + ": " + Opening.quizAnswers[currentquest]);
    }
}
