using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chat
{
    public string sender_usern;
    public string msg;
}

[System.Serializable]
public class ChatObject
{
    public Chat[] messages;
}
