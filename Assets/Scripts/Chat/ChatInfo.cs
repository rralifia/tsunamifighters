using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChatInfo : MonoBehaviour
{
    public static string chat_id { get; private set; }
    public static string senderName { get; private set; }
    public static string message { get; private set; }
    public static string receiver_id { get; private set; }

    // public List<string> chatidList = new List<string>();

    /*private void Start()
    {
        chat_id = string.Empty;
    }*/

    public void SetMessage(string text)
    {
        message = text;
        Debug.Log("Message in ChatInfo: " + message);
    }

    public void SetChatID(string id)
    {
        // id in database - table chat
        chat_id = id;
        // chatidList.Add(chat_id);
        Debug.Log("chat_id in ChatInfo: " + chat_id);
    }

    public void SetSender(string name)
    {
        senderName = name;
        Debug.Log("Sender's Username in ChatInfo: " + senderName);
    }

    public void SetReceiverID (string id)
    {
        receiver_id = id;
        Debug.Log("receiver_id in ChatInfo: " + receiver_id);
    }
}
