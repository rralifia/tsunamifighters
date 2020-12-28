using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoresInfo : MonoBehaviour
{
    //public Button mainmenu_button;
    public static string jijo_score { get; private set; }
    public static string kyojo_score { get; private set; }
    public static string kojo_score { get; private set; }
    public static string game_score { get; private set; }
    public static string pretest_score { get; private set; }
    public static string posttest_score { get; private set; }
    public static string play_id { get; private set; }

    public void SetPlayID(string id)
    {
        // user's id in database - table users
        play_id = id;
        Debug.Log("play_id in ScoresInfo: " + play_id);
    }

    public void SetJijo(string value)
    {
        jijo_score = value;
        Debug.Log("Jijo Score in ScoresInfo: " + jijo_score);
    }

    public void SetKyojo(string value)
    {
        kyojo_score = value;
        Debug.Log("Kyojo Score in ScoresInfo: " + kyojo_score);
    }

    public void SetKojo(string value)
    {
        kojo_score = value;
        Debug.Log("Kojo Score in ScoresInfo: " + kojo_score);
    }

    public void SetGame(string value)
    {
        game_score = value;
        Debug.Log("Game Score in ScoresInfo: " + game_score);
    }

    public void SetPretest (string value)
    {
        pretest_score = value;
        Debug.Log("Pretest Score in ScoresInfo: " + pretest_score);
    }

    public void SetPosttest (string value)
    {
        posttest_score = value;
        Debug.Log("Posttest Score in ScoresInfo: " + posttest_score);
    }

    public void ImportGameScores()
    {
        Debug.Log("Start getting game scores from database");
        Debug.Log("User ID in Import Scores:" + UserInfo.user_id);
        Debug.Log("Play ID in Import Scores:" + play_id);

        Debug.Log("Language in Import Scores: " + UserInfo.language);
        StartCoroutine(Main.Instance.Web.GetPretest(play_id));
        StartCoroutine(Main.Instance.Web.GetKyojo(play_id));
        StartCoroutine(Main.Instance.Web.GetKojo(play_id));
        StartCoroutine(Main.Instance.Web.GetJijo(play_id));
        StartCoroutine(Main.Instance.Web.GetGame(play_id));
    }
}
