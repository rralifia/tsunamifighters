using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{

    // public static instance;
    public static string user_id {get; private set;}
    public static string input_uname, input_pass;
    public static string playerName {get; private set;}
    public static string pretest_id { get; private set; }
    public static string posttest_id { get; private set; }
    public static string gameplay_id { get; private set; }
    public static string playerLoc {get; private set;}
    public static int language; //scene;

    public void SetCredentials(string username, string password)
    {
        // username & password data
        input_uname = username;
        input_pass = password;
    }

    public void SetID(string id)
    {
        // user's id in database - table users
        user_id = id;
        //Debug.Log("user_id in UserInfo: " + user_id);
    }

    public void SetName(string name)
    {
        playerName = name;
        //Debug.Log("playerName in UserInfo: " + playerName);
    }

    public void SetLocation(string loc)
    {
        playerLoc = loc;
        //Debug.Log("playerLoc in UserInfo: " + playerLoc);
    }

    public void SetLanguage(int lang)
    {
        language = lang;
        Debug.Log("Language in UserInfo: " + language);
    }

    public void SetQuizID (string id, string code)
    {
        Debug.Log("Code: " + code);
        if (code == "pretest")
        {
            pretest_id = id;
            Debug.Log("Pretest ID in SetQuizID: " + pretest_id);
        } else if (code == "posttest")
        {
            posttest_id = id;
            Debug.Log("Posttest ID in SetQuizID: " + posttest_id);
        } else
        {
            Debug.Log("Code does not match, pretest or posttest ID can't be set.");
        }
    }

    public void SetGameplayID(string id)
    {
        gameplay_id = id;
        Debug.Log("Gameplay ID in UserInfo: " + gameplay_id);
    }

    public void ReimportData(int value)
    {
        bool reimported = false;
        if (value == 5)
        {
            while (reimported == false)
            {
                Main.Instance.Web.GetLocation(user_id);
                if ((playerLoc != string.Empty) || (playerLoc != " "))
                {
                    Debug.Log("Location is imported.");
                    reimported = true;
                }
            }
            Debug.Log("Location in Reimported Data: " + playerLoc);
        }
        else if (value == 6)
        {
            while (reimported == false)
            {
                Main.Instance.Web.GetLocation(user_id);
                if ((playerLoc != string.Empty) || (playerLoc != " "))
                {
                    Debug.Log("Location is imported.");
                    reimported = true;
                }
            }
            Debug.Log("Location in Reimported Data: " + playerLoc);
        }
    }
}
