using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LearningPage : MonoBehaviour
{
    public GameObject learnMenu, prePage, duringPage, postPage, categoryPage;
    public GameObject backFooter, defaultHeader, additionalHeader, quitBox;

    public Button PrePage, DuringPage, PostPage, CategoryPage, back_button, homeButton, exitButton, testButton;
    public Button quit, cancel;
    int prevscene;


    // Start is called before the first frame update
    void Start()
    {
        /*prevscene = PlayerPrefs.GetInt("SceneNumber");
        Debug.Log("Previous scene: " + prevscene);
        if (prevscene == 4)
        {
            Debug.Log("Start exporting answers...");
            StartCoroutine(Main.Instance.Web.ExportAnswers(Opening.quizAnswers[1], Opening.quizAnswers[2], Opening.quizAnswers[3], Opening.quizAnswers[4], Opening.quizAnswers[5], Opening.quizAnswers[6], Opening.quizAnswers[7], Opening.quizAnswers[8], Opening.quizAnswers[9], Opening.quizAnswers[10], Opening.quizAnswers[11], Opening.quizAnswers[12], Opening.quizAnswers[13], Opening.quizAnswers[14], Opening.quizAnswers[15], Opening.quizAnswers[16], Opening.quizAnswers[17], Opening.quizAnswers[18], Opening.quizAnswers[19], Opening.quizAnswers[20], ScoresInfo.play_id, "pretest"));
        }*/

        ClickToLearningPage();
        learnMenu.SetActive(true);
    }

    public void ClickToLearningPage()
    {
        learnMenu.SetActive(true);
        defaultHeader.SetActive(true);
        backFooter.SetActive(false);
        prePage.SetActive(false);
        duringPage.SetActive(false);
        postPage.SetActive(false);
        categoryPage.SetActive(false);
        quitBox.SetActive(false);

        PrePage.onClick.AddListener(ShowPrePage);
        DuringPage.onClick.AddListener(ShowDuringPage);
        PostPage.onClick.AddListener(ShowPostPage);
        CategoryPage.onClick.AddListener(ShowCategoryPage);
        homeButton.onClick.AddListener(LoadMenu);
        exitButton.onClick.AddListener(Quit);

        prevscene = PlayerPrefs.GetInt("SceneNumber");

        if (prevscene == 3)
        {
            Debug.Log("Previous scene = 3");
            additionalHeader.SetActive(true);
            testButton.onClick.AddListener(ClickToPretest);
        } else if (prevscene == 4)
        {
            additionalHeader.SetActive(true);
            testButton.onClick.AddListener(ClickToPosttest);

            Debug.Log("Previous scene = 4");
            PlayerPrefs.SetInt("SceneNumber", 8);
            Debug.Log("Start exporting answers...");
            StartCoroutine(Main.Instance.Web.ExportAnswers(Opening.quizAnswers[1], Opening.quizAnswers[2], Opening.quizAnswers[3], Opening.quizAnswers[4], Opening.quizAnswers[5], Opening.quizAnswers[6], Opening.quizAnswers[7], Opening.quizAnswers[8], Opening.quizAnswers[9], Opening.quizAnswers[10], Opening.quizAnswers[11], Opening.quizAnswers[12], Opening.quizAnswers[13], Opening.quizAnswers[14], Opening.quizAnswers[15], Opening.quizAnswers[16], Opening.quizAnswers[17], Opening.quizAnswers[18], Opening.quizAnswers[19], Opening.quizAnswers[20], ScoresInfo.play_id, "pretest"));
        } else if (prevscene == 6)
        {
            Debug.Log("Previous scene = 6");
            additionalHeader.SetActive(true);
            testButton.onClick.AddListener(ClickToPosttest);
        } else if (prevscene == 7)
        {
            Debug.Log("Previous scene = 7");
            additionalHeader.SetActive(false);
        }
    }

    void ShowPrePage()
    {
        learnMenu.SetActive(false);
        prePage.SetActive(true);
        backFooter.SetActive(true);
        back_button.onClick.AddListener(ClickToLearningPage);
        quitBox.SetActive(false);
        //Debug.Log("Multiplay ID in Learning Page: " + Multiplay.multiplay_id);
    }

    void ShowDuringPage()
    {
        learnMenu.SetActive(false);
        duringPage.SetActive(true);
        backFooter.SetActive(true);
        back_button.onClick.AddListener(ClickToLearningPage);
        quitBox.SetActive(false);
        //Debug.Log("Multiplay ID in Learning Page: " + Multiplay.multiplay_id);
    }

    void ShowPostPage()
    {
        learnMenu.SetActive(false);
        postPage.SetActive(true);
        backFooter.SetActive(true);
        back_button.onClick.AddListener(ClickToLearningPage);
        quitBox.SetActive(false);
        //Debug.Log("Multiplay ID in Learning Page: " + Multiplay.multiplay_id);
    }

    void ShowCategoryPage()
    {
        learnMenu.SetActive(false);
        categoryPage.SetActive(true);
        backFooter.SetActive(true);
        back_button.onClick.AddListener(ClickToLearningPage);
        quitBox.SetActive(false);
        //Debug.Log("Multiplay ID in Learning Page: " + Multiplay.multiplay_id);
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
            Debug.Log("Connecting to disconnecting...");
        }
    }

    void ClickToPosttest()
    {
        PlayerPrefs.SetInt("SceneNumber", 8);
        int scene = PlayerPrefs.GetInt("SceneNumber");
        Debug.Log("Scene: " + scene);

        SceneManager.LoadScene("Post Test");

        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
            Debug.Log("Connecting to disconnecting...");
        }
    }

    void ClickToPretest()
    {
        SceneManager.LoadScene("Pre Test");
    }

    void Quit()
    {
        quitBox.SetActive(true);
        cancel.onClick.AddListener(ClickToLearningPage);
        quit.onClick.AddListener(Application.Quit);
    }
}
