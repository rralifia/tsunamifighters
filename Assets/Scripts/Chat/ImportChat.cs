using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using TMPro;
using UnityEngine.UI;

public class ImportChat : MonoBehaviour
{

    Action<string> GetChatData;
    string receiverID, senderID;
    public TMP_InputField inputChat;
    public Button backButton, sendButton;
    public GameObject chatbox, box;
    public Chat chat;
    public List<int> indexlist = new List<int>();

    public void StartImport()
    {
        Debug.Log("Start GetChatData...");
        GetChatData = (jsonArrayString) =>
        {
            StartCoroutine(DisplayChat(jsonArrayString));
            Debug.Log("Successfully transferred to GetChatData");
            Debug.Log("json Array in ImportChat: " + jsonArrayString);
        };

        Debug.Log("Starting GetData...");
        GetData();
    }

    public void GetData()
    {
        Debug.Log("Sender ID in GetData: " + senderID);
        Debug.Log("Receiver ID in GetData: " + receiverID);
        Debug.Log("Multiplay ID in GetData: " + Multiplay.multiplay_id);
        // int language = PlayerPrefs.GetInt("Language");
        /*if (ScoresInfo.play_id == senderID)
            {
            StartCoroutine(Main.Instance.Web.ShowChatSender(Multiplay.multiplay_id, GetChatData));
            Debug.Log("Show chat on sender's UI");
        }*/
        if (UserInfo.language == 1)
        {
            StartCoroutine(Main.Instance.Web.ReceiveMessageENG(Multiplay.multiplay_id, GetChatData));
            Debug.Log("Language: English");
        }
        else if (UserInfo.language == 2)
        {
            StartCoroutine(Main.Instance.Web.ReceiveMessageIDN(Multiplay.multiplay_id, GetChatData));
            Debug.Log("Language: Bahasa");
        }
        else if (UserInfo.language == 3)
        {
            StartCoroutine(Main.Instance.Web.ReceiveMessageJPN(Multiplay.multiplay_id, GetChatData));
            Debug.Log("Langunge: Japanese");
        }
    }

    public IEnumerator DisplayChat(string jsonArrayString)
    {
        Debug.Log("Start Parsing...");
        // Parsing jsonArrayString as an array
        JSONArray chatArray = JSON.Parse(jsonArrayString) as JSONArray;
        // array.Add(chatArray);
        // Debug.Log("chatArray.Count: " + chatArray.Count);
        int i;
        for (i = 0; i < chatArray.Count; i++)
        {
            JSONObject chatListJson = new JSONObject();
            // Debug.Log("Looping-" + i);
            // JSONArray chatList = JSON.Parse(jsonArrayString) as JSONArray;
            if (!indexlist.Contains(i))
            {
                Debug.Log("Assigning chatListJson");
                chatListJson = chatArray[i].AsObject;
                indexlist.Add(i);
                //yield return new WaitForSeconds(3.0f);
                yield return new WaitForSeconds(1.0f);

                Debug.Log("Loading MessageBox...");
                // Instantiate GameObject (message prefab)
                chatbox = Instantiate(Resources.Load("Prefabs/MessageBox") as GameObject);
                chatbox.transform.SetParent(box.transform);
                chatbox.transform.localScale = Vector3.one;
                chatbox.transform.localPosition = Vector3.zero;

                Debug.Log("Start Displaying...");
                // Fill information
                chatbox.transform.Find("Sender (TMP)").GetComponent<TextMeshProUGUI>().text = chatListJson["sender_usern"];

                //original message
                chatbox.transform.Find("TextOri (TMP)").GetComponent<TextMeshProUGUI>().text = chatListJson["msg"];
                Debug.Log("Original Message");

                //translated message
                if (UserInfo.language == 1)
                {
                    chatbox.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = chatListJson["msg_en"];
                    Debug.Log("Languange: English");
                }
                else if (UserInfo.language == 2)
                {
                    chatbox.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = chatListJson["msg_idn"];
                    Debug.Log("Languange: Bahasa");
                }
                else if (UserInfo.language == 3)
                {
                    chatbox.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = chatListJson["msg_jpn"];
                    Debug.Log("Languange: Japanese");
                }
            }
            
        }
    }

    public void Begin()
    {
        Debug.Log("Multiplay ID: " + Multiplay.multiplay_id);
        Debug.Log("playidA: " + NovelController.playidA + " & playidB: " + NovelController.playidB);
        Debug.Log("Sender name: " + UserInfo.playerName);

        if (NovelController.idNetwork == 1) // player 1
        {
            senderID = NovelController.playidA;
            receiverID = NovelController.playidB;
        }
        else if (NovelController.idNetwork == 2) //player 2
        {
            senderID = NovelController.playidB;
            receiverID = NovelController.playidA;
        }

        Debug.Log("id Network: " + NovelController.idNetwork);
        Debug.Log("Sender ID: " + senderID);
        Debug.Log("Receiver ID: " + receiverID);

        // InvokeRepeating("StartImport", 5.0f, 5.0f);
        InvokeRepeating("StartImport", 1.0f, 1.0f);

        sendButton.onClick.AddListener(SendChat);
        backButton.onClick.AddListener(LoadGame);
    }

    public void SendChat()
    {
        Debug.Log("Text: " + inputChat.text);
        Debug.Log("Sender Name: " + UserInfo.playerName);
        StartCoroutine(Main.Instance.Web.AddMessage(inputChat.text, senderID, receiverID, UserInfo.playerName, Multiplay.multiplay_id));
        Debug.Log("Message is exported.");
        // StartImport();
        inputChat.text = string.Empty;
        PlayerNetwork.instance.ChatNotification();
    }

    public void LoadGame()
    {
        NovelController.instance.chatCanvas.SetActive(false);
    }
}