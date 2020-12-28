using UnityEngine;
using TMPro;

public class CurrentRoomCanvas : MonoBehaviour
{
    public TextMeshProUGUI status, greetingText;
    public GameObject StartGame, DisconnectedPanel, homeButton;

    private void Update()
    {
        if (PhotonNetwork.inRoom)
        {
            if (UserInfo.language == 2)
            {
                greetingText.text = "Halo, Pemain " + PhotonNetwork.player.ID;
            } else if (UserInfo.language == 1)
            {
                greetingText.text = "Hello, Player " + PhotonNetwork.player.ID;
            } else if (UserInfo.language == 3)
            {
                greetingText.text = "Konichiwa, Player " + PhotonNetwork.player.ID;
            }

            if (PhotonNetwork.playerList.Length == 2)
            {
                //Close the room if there has already two players in the room
                PhotonNetwork.room.IsOpen = false;
                PhotonNetwork.room.IsVisible = false;
                if(PhotonNetwork.player.ID == 1) {
                    if (UserInfo.language == 1)
                        status.text = "You can start the game.";
                    else if (UserInfo.language == 2)
                        status.text = "Kamu bisa memulai permainan.";
                    else if (UserInfo.language == 3)
                        status.text = "You can start the game.";
                }
                else if (PhotonNetwork.player.ID == 2)
                {
                    if (UserInfo.language == 1)
                        status.text = "Waiting the host starting the game.";
                    else if (UserInfo.language == 2)
                        status.text = "Menunggu pembuat ruangan memulai permainan.";
                    else if (UserInfo.language == 3)
                        status.text = "Waiting the host starting the game.";
                }
                homeButton.SetActive(false);
            }
            else
            if (PhotonNetwork.playerList.Length == 1)
            {
                if (UserInfo.language == 1)
                {
                    status.text = "Waiting for another player.";
                }
                else if (UserInfo.language == 2)
                {
                    status.text = "Menunggu pemain lain bergabung.";
                }
                else if (UserInfo.language == 3)
                {
                    status.text = "Waiting for another player.";
                }
                StartGame.SetActive(false);
                homeButton.SetActive(true);
            }

            //Show the start game button for the player who create the room if there has already two players in the room 
            if (PhotonNetwork.isMasterClient && PhotonNetwork.playerList.Length == 2)
            {
                StartGame.SetActive(true);
            }

            if (!PhotonNetwork.connected && PhotonNetwork.connecting)
                MainCanvasManager.instance.DisconnectedPanel.transform.SetAsLastSibling();
        }
    }

    //Called when any player in the room disconnected from the photon server
    private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer)
    {
        MainCanvasManager.instance.DisconnectedPanel.transform.SetAsLastSibling();
    }

    //Start directly
    public void OnClickStartGame()
    {
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.LoadLevel(6);
    }

    //Start delayed
    public void OnClickStartDelayed()
    {
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.LoadLevel(6);
    }

    public void OnClickNovelMenuButton()
    {
        //this.DisconnectedPanel.gameObject.SetActive(false);
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
        }
        StartGame.SetActive(false);
        MainCanvasManager.instance.LobbyCanvas.transform.SetAsLastSibling();
    }
}
