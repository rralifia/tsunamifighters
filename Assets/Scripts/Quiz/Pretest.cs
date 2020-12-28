using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pretest : MonoBehaviour
{
    // public Button start_game;
    public int pretest;
    public GameObject score_label, progress, score_value;

    void Start()
    {
        pretest = PlayerPrefs.GetInt("QuizScore");
        Debug.Log("Pre-test score: " + pretest);
        score_label.SetActive(true);
        score_value.SetActive(true);
        progress.SetActive(false);
        StartCoroutine(Main.Instance.Web.AddPretest(pretest));

        PlayerPrefs.SetInt("SceneNumber", 4);
        int scene = PlayerPrefs.GetInt("SceneNumber");
        Debug.Log("Scene Number in Pretest: " + scene);
        
    }

    public void PlayNow()
    {
        //Debug.Log("Start exporting answers...");
        //StartCoroutine(Main.Instance.Web.ExportAnswers(Opening.quizAnswers[1], Opening.quizAnswers[2], Opening.quizAnswers[3], Opening.quizAnswers[4], Opening.quizAnswers[5], Opening.quizAnswers[6], Opening.quizAnswers[7], Opening.quizAnswers[8], Opening.quizAnswers[9], Opening.quizAnswers[10], Opening.quizAnswers[11], Opening.quizAnswers[12], Opening.quizAnswers[13], Opening.quizAnswers[14], Opening.quizAnswers[15], Opening.quizAnswers[16], Opening.quizAnswers[17], Opening.quizAnswers[18], Opening.quizAnswers[19], Opening.quizAnswers[20], ScoresInfo.play_id, "pretest"));
        SceneManager.LoadScene("Novel Menu");

        //PlayerPrefs.SetInt("SceneNumber", 5);
        int scene = PlayerPrefs.GetInt("SceneNumber");
        Debug.Log("Scene Number in Next Scene - Pretest: " + scene);
        PhotonNetwork.ConnectUsingSettings("0");
    }

    public void LearnNow()
    {
        //Debug.Log("Start exporting answers...");
        //StartCoroutine(Main.Instance.Web.ExportAnswers(Opening.quizAnswers[1], Opening.quizAnswers[2], Opening.quizAnswers[3], Opening.quizAnswers[4], Opening.quizAnswers[5], Opening.quizAnswers[6], Opening.quizAnswers[7], Opening.quizAnswers[8], Opening.quizAnswers[9], Opening.quizAnswers[10], Opening.quizAnswers[11], Opening.quizAnswers[12], Opening.quizAnswers[13], Opening.quizAnswers[14], Opening.quizAnswers[15], Opening.quizAnswers[16], Opening.quizAnswers[17], Opening.quizAnswers[18], Opening.quizAnswers[19], Opening.quizAnswers[20], ScoresInfo.play_id, "pretest"));
        SceneManager.LoadScene("Learning");
    }
}
