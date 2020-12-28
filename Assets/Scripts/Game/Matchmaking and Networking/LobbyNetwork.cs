using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyNetwork : MonoBehaviour
{
    public GameObject createRoom, roomInput, roomListTitle, roomList;
    public TextMeshProUGUI networkStatus, errorMessage;

    [SerializeField]
    private TMP_InputField _roomName;
    private TMP_InputField RoomName
    {
        get { return _roomName; }
    }

    // Start is called before the first frame update
    private void Start()
    {
        /*if (PhotonNetwork.connected)
            PhotonNetwork.Disconnect();*/
        //Check the user and scores info
        Debug.Log("Play ID in Lobby: " + ScoresInfo.play_id);
        Debug.Log("Location in Lobby: " + UserInfo.playerLoc);
        Debug.Log("Language in Lobby: " + UserInfo.language);

        int prevscene = PlayerPrefs.GetInt("SceneNumber");
        if (prevscene == 4)
        {
            PlayerPrefs.SetInt("SceneNumber", 5);
            Debug.Log("Start exporting answers...");
            StartCoroutine(Main.Instance.Web.ExportAnswers(Opening.quizAnswers[1], Opening.quizAnswers[2], Opening.quizAnswers[3], Opening.quizAnswers[4], Opening.quizAnswers[5], Opening.quizAnswers[6], Opening.quizAnswers[7], Opening.quizAnswers[8], Opening.quizAnswers[9], Opening.quizAnswers[10], Opening.quizAnswers[11], Opening.quizAnswers[12], Opening.quizAnswers[13], Opening.quizAnswers[14], Opening.quizAnswers[15], Opening.quizAnswers[16], Opening.quizAnswers[17], Opening.quizAnswers[18], Opening.quizAnswers[19], Opening.quizAnswers[20], ScoresInfo.play_id, "pretest"));
        }
        //Connecting to photon server
        //print("Connecting to server...");
        //PhotonNetwork.ConnectUsingSettings("0");
    }

    private void Update()
    {
        //Shows the network status in the UI
        if (PhotonNetwork.connected)
        {
            if (UserInfo.language == 1)
                networkStatus.text = "Connected";
            else if (UserInfo.language == 2)
                networkStatus.text = "Terhubung";
            else if (UserInfo.language == 3)
                networkStatus.text = "Connected";
        }
        else
        if (!PhotonNetwork.connected && !PhotonNetwork.connecting)
        {
            if (UserInfo.language == 1)
                networkStatus.text = "Disconnected";
            else if (UserInfo.language == 2)
                networkStatus.text = "Terputus";
            else if (UserInfo.language == 3)
                networkStatus.text = "Disconnected";
            PhotonNetwork.ConnectUsingSettings("0");
        }
        else
        if (!PhotonNetwork.connected && PhotonNetwork.connecting)
        {
            if (UserInfo.language == 1)
                networkStatus.text = "Connecting";
            else if (UserInfo.language == 2)
                networkStatus.text = "Menghubungkan";
            else if (UserInfo.language == 3)
                networkStatus.text = "Connecting";
        }
    }

    //Called when the player has connected to the photon server
    private void OnConnectedToMaster()
    {
        print("Connected to master.");
        PhotonNetwork.automaticallySyncScene = false;
        //PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.playerName = PlayerNetwork.instance.PlayerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    //Called when the player failed to connect to photon server
    private void OnFailedToConnectToPhoton(object[] codeAndMessage)
    {
        Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + codeAndMessage[1] + " ServerAddress: " + PhotonNetwork.ServerAddress);
        //Try to connect again
        PhotonNetwork.ConnectUsingSettings("0");
    }

    public static string userA, userB;

    //Called when the player has joined the lobby
    private void OnJoinedLobby()
    {
        print("Joined lobby.");

        createRoom.SetActive(true);
        roomInput.SetActive(true);
        //roomList.SetActive(true);
        //roomListTitle.SetActive(true);
        /*string playerLoc = UserInfo.playerLoc;
        Debug.Log("Location: " + playerLoc);

        //Check the language chosen by the player
        if (playerLoc == " Indonesia")
        {
            //Show the input field and create room button
            createRoom.SetActive(true);
            roomInput.SetActive(true);
        }
        else
        {
            //Show the roomlist
            roomList.SetActive(true);
            roomListTitle.SetActive(true);
        }*/

        //Change the lobby as last sibling (send the lobby layer to forward) if the player wasn't in the room.
        if (!PhotonNetwork.inRoom)
            MainCanvasManager.instance.LobbyCanvas.transform.SetAsLastSibling();
    }

    //Run if create room button clicked
    public void OnClick_CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };

        if(string.IsNullOrEmpty(RoomName.text))
        {
            if (UserInfo.language == 1)
                errorMessage.text = "Please fill the room name field";
            else if (UserInfo.language == 2)
                errorMessage.text = "Silakan isi kolom nama ruang";
            else if (UserInfo.language == 3)
                errorMessage.text = "Please fill the room name field";
        }
        else
        {
            //Create the room based on the roomOptions
            if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
            {
                print("Create room successfully sent.");
                if (UserInfo.language == 1)
                    errorMessage.text = "Create room " + RoomName.text + " request sent";
                else if (UserInfo.language == 2)
                    errorMessage.text = "Permintaan pembuatan ruang " + RoomName.text + " terkirim";
                else if (UserInfo.language == 3)
                    errorMessage.text = "Create room " + RoomName.text + " request sent";
            }
            else
            {
                print("Create room failed to send.");
                if (UserInfo.language == 1)
                    errorMessage.text = "Create room request failed to sent";
                else if (UserInfo.language == 2)
                    errorMessage.text = "Permintaan pembuatan ruang gagal";
                else if (UserInfo.language == 3)
                    errorMessage.text = "Create room request failed to sent";
            }
        }
    }

    //Called when create room failed
    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        print("Create room failed: " + codeAndMessage[1]);
    }

    //Called when room successfully created
    private void OnCreatedRoom()
    {
        print("Room " + RoomName.text + " created successfully.");
    }

    //Called when player has joined the room
    private void OnJoinedRoom()
    {
        //Change the currentroomcanvas as last sibling (send the currentroomcanvas layer to forward).
        MainCanvasManager.instance.CurrentRoomCanvas.transform.SetAsLastSibling();
        errorMessage.text = "";
        /*createRoom.SetActive(false);
        roomInput.SetActive(false);
        roomList.SetActive(false);
        roomListTitle.SetActive(false);*/
    }
}
