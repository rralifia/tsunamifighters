using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer1 : MonoBehaviour
{
    // Start is called before the first frame update
    public void answers(bool Answer1)
    {
        if (Answer1)
        {
            // if true, got 10 more points
            int score = PlayerPrefs.GetInt("QuizScore") +  10;
            PlayerPrefs.SetInt("QuizScore", score);
        }
    }
}
