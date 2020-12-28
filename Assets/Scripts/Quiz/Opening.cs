using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    public GameObject score_label, opening, question, disconnectedPanel, header, progress, score_value;
    public GameObject helpPanel, quitBox;
    public Button start_test, restartButton, helpButton, quitButton;
    public Button resumeButton, homeButton, quit, cancel;
    public static string[] quizAnswers = new string[21];

    // Start is called before the first frame update
    void Start()
    {
        /*if (PhotonNetwork.connected)
            PhotonNetwork.Disconnect();*/
        score_label.SetActive(false);
        score_value.SetActive(false);
        progress.SetActive(false);
        opening.SetActive(true);
        header.SetActive(true);
        quitBox.SetActive(false);

        int prevscene = PlayerPrefs.GetInt("SceneNumber");
        Debug.Log("Previous scene: " + prevscene);
        if (prevscene == 8)
        {
            StartCoroutine(Main.Instance.Web.AddMultiplay(ScoresInfo.play_id, "x"));
        }

        start_test.onClick.AddListener(BeginTest);
        restartButton.onClick.AddListener(Restart);
        helpButton.onClick.AddListener(Help);
        quitButton.onClick.AddListener(Quit);


        Debug.Log("Initializing array of answers...");
        for (int i = 0; i < 21; i++)
        {
            quizAnswers[i] = null;
        }
    }

    void BeginTest()
    {
        opening.SetActive(false);
        question.SetActive(true);
        progress.SetActive(true);
        score_label.SetActive(true);
        score_value.SetActive(true);
        disconnectedPanel.SetActive(false);
        header.SetActive(true);
        quitBox.SetActive(false);
    }

    void Restart()
    {
        PlayerPrefs.SetInt("QuestNumber", 1);
        gameObject.SetActive(false);
        opening.SetActive(true);
        header.SetActive(true);
        question.SetActive(false);
        score_label.SetActive(false);
        score_value.SetActive(false);
        disconnectedPanel.SetActive(false);
        quitBox.SetActive(false);
    }

    void Help()
    {
        helpPanel.SetActive(true);
        Time.timeScale = 0;
        resumeButton.onClick.AddListener(ResumeGame);
        homeButton.onClick.AddListener(LoadMenu);
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        helpPanel.SetActive(false);
        quitBox.SetActive(false);
    }

    void LoadMenu()
    {
        // if user clicks main menu button, the scene will become Main Menu scene
        Time.timeScale = 1;
        gameObject.SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }

    void Quit()
    {
        Time.timeScale = 0;
        quitBox.SetActive(true);
        cancel.onClick.AddListener(ResumeGame);
        quit.onClick.AddListener(Application.Quit);
    }
}
