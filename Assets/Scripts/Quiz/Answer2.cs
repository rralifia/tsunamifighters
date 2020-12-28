using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer2 : MonoBehaviour
{
    // Start is called before the first frame update
    public void answers(bool Answer2)
    {
        if (Answer2)
        {
            // if true, got 20 more points
            int score = PlayerPrefs.GetInt("QuizScore") + 20;
            PlayerPrefs.SetInt("QuizScore", score);
        }
    }
}
