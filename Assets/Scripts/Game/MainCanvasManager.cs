using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCanvasManager : MonoBehaviour
{
    public static MainCanvasManager instance;
    
    [SerializeField]
    private LobbyCanvas _lobbyCanvas;
    public LobbyCanvas LobbyCanvas
    {
        get { return _lobbyCanvas; }
    }

    [SerializeField]
    private CurrentRoomCanvas _currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas
    {
        get { return _currentRoomCanvas; }
    }

    [SerializeField]
    private GameObject _disconnectedPanel;
    public GameObject DisconnectedPanel
    {
        get { return _disconnectedPanel; }
    }

    [SerializeField]
    private GameObject _backMenuPanel;
    public GameObject BackMenuPanel
    {
        get { return _backMenuPanel; }
    }

    private void Awake()
    {
        instance = this;
        /*if (PhotonNetwork.connected)
            PhotonNetwork.Disconnect();*/
    }
}