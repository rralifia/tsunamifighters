using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    public static Main Instance;

    public WebRequest Web;
    public UserInfo UserInfo;
    public Login Login;
    public Register Register;
    public Opening QuizOpening;
    public ScoresInfo ScoresInfo;
    public Multiplay Multiplay;
    public ChatInfo ChatInfo;
    public ImportChat Chat;
    public NovelController Novel;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Web = GetComponent<WebRequest>();
        UserInfo = GetComponent<UserInfo>();
        Register = GetComponent<Register>();
        ScoresInfo = GetComponent<ScoresInfo>();
        Multiplay = GetComponent<Multiplay>();
        ChatInfo = GetComponent<ChatInfo>();
        Chat = GetComponent<ImportChat>();
        Novel = GetComponent<NovelController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}