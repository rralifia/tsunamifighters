using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiplay : MonoBehaviour
{

    public static string multiplay_id { get; private set; }
    public static string id_A { get; private set; }
    public static string id_B { get; private set; }
    // string idA, idB;

    public GameObject disconnectPanel;
    public Button reconnectButton, quitButton;
    // Start is called before the first frame update
    public void Disconnected()
    {
        if (!PhotonNetwork.connected && !PhotonNetwork.connecting)
        {
            disconnectPanel.SetActive(true);
            quitButton.onClick.AddListener(NovelController.instance.BackNovelMenu);
            reconnectButton.onClick.AddListener(Reconnect);
        }
        else if (!PhotonNetwork.connected && PhotonNetwork.connecting)
        {
            Debug.Log("Please wait, reconnecting");
        }
    }

    void Reconnect()
    {
        StartCoroutine(Main.Instance.Web.FindMultiplayA(multiplay_id));
        StartCoroutine(Main.Instance.Web.FindMultiplayB(multiplay_id));
        PhotonNetwork.ConnectUsingSettings("0");
        StartCoroutine(UpdatePlayID());
    }

    IEnumerator UpdatePlayID()
    {
        yield return new WaitForSeconds(3.0f);
        NovelController.playidA = id_A;
        NovelController.playidB = id_B;

        Debug.Log("Novel-playidA after Reconnect: " + NovelController.playidA);
        Debug.Log("Novel-playidB after Reconnect: " + NovelController.playidB);

        ResetScores();
    }

    void ResetScores()
    {
        StartCoroutine(Main.Instance.Web.GetGame(id_A));
        StartCoroutine(Main.Instance.Web.GetJijo(id_A));
        StartCoroutine(Main.Instance.Web.GetKyojo(id_A));
        StartCoroutine(Main.Instance.Web.GetKyojo(id_A));
        StartCoroutine(Main.Instance.Web.GetGame(id_B));
        StartCoroutine(Main.Instance.Web.GetKyojo(id_B));
        StartCoroutine(Main.Instance.Web.GetKyojo(id_B));
        StartCoroutine(Main.Instance.Web.GetKyojo(id_B));
    }

    IEnumerator UpdateScores()
    {
        yield return new WaitForSeconds(3.0f);
        NovelController.scoreA = int.Parse(ScoresInfo.game_score);
        NovelController.jijoA = int.Parse(ScoresInfo.jijo_score);
        NovelController.kyojoA = int.Parse(ScoresInfo.kyojo_score);
        NovelController.kojoA = int.Parse(ScoresInfo.kojo_score);
        NovelController.scoreB = int.Parse(ScoresInfo.game_score);
        NovelController.jijoB = int.Parse(ScoresInfo.jijo_score);
        NovelController.kyojoB = int.Parse(ScoresInfo.kyojo_score);
        NovelController.kojoB = int.Parse(ScoresInfo.kojo_score);
    }

    public void SetMultiplay(string id)
    {
        multiplay_id = id;
        Debug.Log("multiplay_id in Multiplay: " + multiplay_id);
    }

    public void SetIdA(string id)
    {
        id_A = id;
        Debug.Log("id_A in Multiplay: " + multiplay_id);
    }

    public void SetIdB(string id)
    {
        id_B = id;
        Debug.Log("id_B in Multiplay: " + multiplay_id);
    }
}
