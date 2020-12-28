using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer3 : MonoBehaviour
{
    public void answers(bool Answer3)
    {
        if (Answer3)
        {
            // if true, got 30 more points
            int score = PlayerPrefs.GetInt("QuizScore") + 30;
            PlayerPrefs.SetInt("QuizScore", score);
        }
    }
}
