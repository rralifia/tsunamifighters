using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Posttest : MonoBehaviour
{
    // public Button show_button;
    public int posttest;
    public static string posttest_score;
    public GameObject score_label, progress, score_value;
    public TextMeshProUGUI comparisonText;
    string comparison;
    // int language;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Multiplay ID in Posttest: " + Multiplay.multiplay_id);
        posttest = PlayerPrefs.GetInt("QuizScore");
        score_label.SetActive(true);
        score_value.SetActive(true);
        progress.SetActive(false);

        //export scores
        StartCoroutine(Main.Instance.Web.AddPosttest(posttest, ScoresInfo.play_id, UserInfo.user_id));

        posttest_score = posttest.ToString();
        Debug.Log("Posttest Score after String: " + posttest_score);

        PlayerPrefs.SetInt("SceneNumber", 7);
        int scene = PlayerPrefs.GetInt("SceneNumber");
        Debug.Log("Scene Number in Posttest: " + scene);

        Debug.Log("Start comparing...");
        CompareTest();
    }

    public void CompareTest()
    {
        int score1 = int.Parse(ScoresInfo.pretest_score);
        int score2 = PlayerPrefs.GetInt("QuizScore");
        /*Debug.Log("Score1 in Comparison: " + score1);
        Debug.Log("Score2 in Comparison: " + score2);
        Debug.Log("Language chosen in Comparison: " + UserInfo.language);*/

        if (UserInfo.language == 2)
        {
            if (score2 < score1)
            {
                //comparisonText.GetComponent<TextMeshProUGUI>().text = "MENURUN";
                comparison = "Menurun";
                Debug.Log(comparison);
            }
            else if (score2 > score1)
            {
                //comparisonText.GetComponent<TextMeshProUGUI>().text = "MENINGKAT";
                comparison = "Meningkat";
                Debug.Log(comparison);
            }
            else if (score2 == score1)
            {
                //comparisonText.GetComponent<TextMeshProUGUI>().text = "SAMA";
                comparison = "Sama";
                Debug.Log(comparison);
            }
        }
        else if (UserInfo.language == 1)
        {
            if (score2 < score1)
            {
                //comparisonText.GetComponent<TextMeshProUGUI>().text = "DECREASED";
                comparison = "Diminished";
                Debug.Log(comparison);
            }
            else if (score2 > score1)
            {
                //comparisonText.GetComponent<TextMeshProUGUI>().text = "IMPROVED";
                comparison = "Improved";
                Debug.Log(comparison);
            }
            else if (score2 == score1)
            {
                //comparisonText.GetComponent<TextMeshProUGUI>().text = "SAME";
                comparison = "Same";
                Debug.Log(comparison);
            }
        }
        else if (UserInfo.language == 3)
        {
            if (score2 < score1)
            {
                //comparisonText.GetComponent<TextMeshProUGUI>().text = "DECREASED";
                comparison = "減少した";
                Debug.Log(comparison);
            }
            else if (score2 > score1)
            {
                //comparisonText.GetComponent<TextMeshProUGUI>().text = "IMPROVED";
                comparison = "改善された";
                Debug.Log(comparison);
            }
            else if (score2 == score1)
            {
                //comparisonText.GetComponent<TextMeshProUGUI>().text = "SAME";
                comparison = "同じ";
                Debug.Log(comparison);
            }
        }
        else
        {
            //comparisonText.GetComponent<TextMeshProUGUI>().text = "NOT FOUND";
            comparison = "Not found";
            Debug.Log(comparison);
        }
            comparisonText.GetComponent<TextMeshProUGUI>().text = comparison;
        }
}