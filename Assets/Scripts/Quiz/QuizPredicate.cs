using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizPredicate : MonoBehaviour
{
    public TextMeshProUGUI predicateText;
    string predicate;

    // Start is called before the first frame update
    void Start()
    {
        int score = PlayerPrefs.GetInt("QuizScore");
        Debug.Log("Score in Predicate: " + score);
        Debug.Log("Language chosen in Predicate: " + UserInfo.language);

        if (UserInfo.language == 2)
        {
            if (score <= 110)
            {
                //predicateText.GetComponent<TextMeshProUGUI>().text = "TIDAK SIAP";
                predicate = "Tidak siap";
                Debug.Log(predicate);
            }
            else if (score > 110 && score <= 220)
            {
                //predicateText.GetComponent<TextMeshProUGUI>().text = "KURANG SIAP";
                predicate = "Kurang siap";
                Debug.Log(predicate);
            }
            else if (score > 220 && score <= 330)
            {
                //predicateText.GetComponent<TextMeshProUGUI>().text = "SIAP";
                predicate = "Siap";
                Debug.Log(predicate);
            }
            else if (score > 330)
            {
                //predicateText.GetComponent<TextMeshProUGUI>().text = "SANGAT SIAP";
                predicate = "Sangat siap";
                Debug.Log(predicate);
            }
        }
        else if (UserInfo.language == 1)
        {
            if (score <= 110)
            {
                //predicateText.GetComponent<TextMeshProUGUI>().text = "UNPREPARED";
                predicate = "Unprepared";
                Debug.Log(predicate);
            }
            else if (score > 110 && score <= 220)
            {
                //predicateText.GetComponent<TextMeshProUGUI>().text = "LESS PREPARED";
                predicate = "Less prepared";
                Debug.Log(predicate);
            }
            else if (score > 220 && score <= 330)
            {
                //predicateText.GetComponent<TextMeshProUGUI>().text = "PREPARED";
                predicate = "Prepared";
                Debug.Log(predicate);
            }
            else if (score > 330)
            {
                //predicateText.GetComponent<TextMeshProUGUI>().text = "VERY PREPARED";
                predicate = "Very prepared";
                Debug.Log(predicate);
            }
        }
        else if (UserInfo.language == 3)
        {
            if (score <= 110)
            {
                //predicateText.GetComponent<TextMeshProUGUI>().text = "UNPREPARED";
                predicate = "準備ができていません";
                Debug.Log(predicate);
            }
            else if (score > 110 && score <= 220)
            {
                //predicateText.GetComponent<TextMeshProUGUI>().text = "LESS PREPARED";
                predicate = "準備が少し足りません";
                Debug.Log(predicate);
            }
            else if (score > 220 && score <= 330)
            {
                //predicateText.GetComponent<TextMeshProUGUI>().text = "PREPARED";
                predicate = "準備できています";
                Debug.Log(predicate);
            }
            else if (score > 330)
            {
                //predicateText.GetComponent<TextMeshProUGUI>().text = "VERY PREPARED";
                predicate = "とてもよく準備ができています";
                Debug.Log(predicate);
            }
        }
        else
        {
            //predicateText.GetComponent<TextMeshProUGUI>().text = "NOT FOUND";
            predicate = "Not found";
            Debug.Log(predicate);
        }
        predicateText.GetComponent<TextMeshProUGUI>().text = predicate;
    }
}
