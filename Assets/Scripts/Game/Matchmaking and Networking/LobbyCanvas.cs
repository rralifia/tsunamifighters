using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour
{
    [SerializeField]
    private RoomLayoutGroup _roomLayoutGroup;
    public RoomLayoutGroup RoomLayoutGroup
    {
        get { return _roomLayoutGroup; }
    }

    public void OnClickJoinRoom(string roomName)
    {
        //to check whether the player has joined the room.
        if (PhotonNetwork.JoinRoom(roomName))
        {
            print("Joined to room" + roomName);
        }
        else
        {
            print("Join room failed.");
        }
    }

    public void OnClickHome()
    {
        MainCanvasManager.instance.BackMenuPanel.transform.SetAsLastSibling();
    }

    public void CloseBackMenuPanel()
    {
        MainCanvasManager.instance.LobbyCanvas.transform.SetAsLastSibling();
        Application.Quit();
    }

    public void OnClickMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
        /*if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
            Debug.Log("Connecting to disconnecting...");
        }*/
    }
}
