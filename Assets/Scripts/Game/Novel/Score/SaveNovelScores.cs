using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveNovelScores : MonoBehaviour
{

    public GameObject scoreA, scoreB, jijoA, jijoB, kyojoA, kyojoB, kojoA, kojoB;

    // Start is called before the first frame update
    void Start()
    {

        PlayerPrefs.SetInt("SceneNumber", 6);
        int scene = PlayerPrefs.GetInt("SceneNumber");
        Debug.Log("Scene Number in Novel: " + scene);
        // Showing scores to UI
        scoreA.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("ScoreA").ToString();
        scoreB.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("ScoreB").ToString();
        jijoA.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("JijoA").ToString();
        jijoB.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("JijoB").ToString();
        kyojoA.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("KyojoA").ToString();
        kyojoB.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("KyojoB").ToString();
        kojoA.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("KojoA").ToString();
        kojoB.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("KojoB").ToString();
        
        /*if (NovelController.idNetwork == 1)
        {
            Debug.Log(NovelController.idNetwork + " is exporting Novel Scores in SaveGameScores...");
            StartCoroutine(Main.Instance.Web.AddNovelScores(NovelController.jijoA, NovelController.kyojoA, NovelController.kojoA, NovelController.scoreA, ScoresInfo.play_id));

            //StartCoroutine(Main.Instance.Web.AddJijo(ScoresInfo.play_id, NovelController.jijoA));
            //StartCoroutine(Main.Instance.Web.AddKojo(ScoresInfo.play_id, NovelController.kojoA));
            //StartCoroutine(Main.Instance.Web.AddKyojo(ScoresInfo.play_id, NovelController.kyojoA));
            //StartCoroutine(Main.Instance.Web.AddGame(ScoresInfo.play_id, NovelController.scoreA));*
        }
        else if (NovelController.idNetwork == 2)
        {
            Debug.Log(NovelController.idNetwork + " is exporting Novel Scores in SaveGameScores...");
            StartCoroutine(Main.Instance.Web.AddNovelScores(NovelController.jijoB, NovelController.kyojoB, NovelController.kojoB, NovelController.scoreB, ScoresInfo.play_id));

            //StartCoroutine(Main.Instance.Web.AddJijo(ScoresInfo.play_id, NovelController.jijoB));
            //StartCoroutine(Main.Instance.Web.AddKojo(ScoresInfo.play_id, NovelController.kojoB));
            //StartCoroutine(Main.Instance.Web.AddKyojo(ScoresInfo.play_id, NovelController.kyojoB));
            //StartCoroutine(Main.Instance.Web.AddGame(ScoresInfo.play_id, NovelController.scoreB));
        }*/

        Debug.Log("Scores in Count Score");
        Debug.Log(NovelController.scoreA + ", " + NovelController.jijoA + ", " + NovelController.kyojoA + ", " + NovelController.kojoA);
        Debug.Log(NovelController.scoreB + ", " + NovelController.jijoB + ", " + NovelController.kyojoB + ", " + NovelController.kojoB);

    }

    public void BeginPosttest()
    {
        /*if (PhotonNetwork.connected)
            PhotonNetwork.Disconnect();*/
        SceneManager.LoadScene("Post Test");
        PlayerPrefs.SetInt("SceneNumber", 7);
        int scene = PlayerPrefs.GetInt("SceneNumber");
        Debug.Log("Scene Number in NextScene - SaveGameScores: " + scene);
    }

    public void LearnFirst()
    {
        /*if (PhotonNetwork.connected)
            PhotonNetwork.Disconnect();*/
        SceneManager.LoadScene("Learning");
    }
}
