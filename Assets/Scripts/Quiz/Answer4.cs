using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer4 : MonoBehaviour
{
    public void answers(bool Answer4)
    {
        if (Answer4)
        {
            // if true, got 40 more points
            int score = PlayerPrefs.GetInt("QuizScore") + 40;
            PlayerPrefs.SetInt("QuizScore", score);
        }
    }
}
