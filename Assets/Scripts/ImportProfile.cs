using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

public class ImportProfile : MonoBehaviour
{

    Action<string> GetPlayerData;

    // Start is called before the first frame update
    public GameObject playerData;
    public Button back_button;
    //string textJson;
    //public static string name;

    void Start()
    {
        GetPlayerData = (jsonString) =>
        {
            //textJson = jsonString;
            Debug.Log("Successfully transferred to GetPlayerData");
            Debug.Log("json String in ImportProfile: "+jsonString);
            StartCoroutine(DataRoutine(jsonString));
        };
    }

    public void ShowData()
    {
        // Debug.Log("user_id in ShowData(): "+UserInfo.user_id);
        // Debug.Log("GetPlayerData: " + GetPlayerData);
        StartCoroutine(Main.Instance.Web.GetProfile(UserInfo.user_id, GetPlayerData));
    }

    IEnumerator DataRoutine(string jsonString)
    {
        //Debug.Log("Start parsing...");
        // Parsing
        for (int i = 0; i < 1; i++)
        {
            JSONObject playerInfoJson = new JSONObject();
            JSONArray playerInfo = JSON.Parse(jsonString) as JSONArray;
            playerInfoJson = playerInfo[0].AsObject;

            //Debug.Log("Waiting for 3 seconds...");
            yield return new WaitForSeconds(1.0f);

            Debug.Log("Creating GameObject...");
            // Instantiate GameObject
            playerData = Instantiate(Resources.Load("Prefabs/Data") as GameObject);
            playerData.transform.SetParent(this.transform);
            playerData.transform.localScale = Vector3.one;
            playerData.transform.localPosition = Vector3.zero;

            // Fill Information
            playerData.transform.Find("Name (TMP)").GetComponent<TextMeshProUGUI>().text = playerInfoJson["name"];
            playerData.transform.Find("Username (TMP)").GetComponent<TextMeshProUGUI>().text = playerInfoJson["username"];
            playerData.transform.Find("Age (TMP)").GetComponent<TextMeshProUGUI>().text = playerInfoJson["age"];
            playerData.transform.Find("Gender (TMP)").GetComponent<TextMeshProUGUI>().text = playerInfoJson["gender"];
            playerData.transform.Find("Location (TMP)").GetComponent<TextMeshProUGUI>().text = playerInfoJson["location"];

            back_button.onClick.AddListener(GoToMain);
        }
    }

    public void GoToMain()
    {
        Destroy(playerData);
    }
}
