using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork instance;
    public string PlayerName { get; private set; }
    public string PlayID;
    private PhotonView PhotonView;
    private int PlayersInGame = 0;
    //public GameObject DisconnectedPanel;

    private void Awake()
    {
        instance = this;
        PhotonView = GetComponent<PhotonView>();
        Debug.Log("User ID in PlayerNetwork: " + UserInfo.user_id);
        Debug.Log("Play ID in PlayerNetwork: " + ScoresInfo.play_id);
        PlayerName = UserInfo.playerName;
        Debug.Log("Player Name in PlayerNetwork: " + PlayerName);

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void Update()
    {
        //Shows the Disconnected Panel if the player disconnected from the photon server
        /*if (!PhotonNetwork.connected && !PhotonNetwork.connecting)
        {
            this.DisconnectedPanel.gameObject.SetActive(true);
        }*/
    }

    //To load scene
    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Novel")
        {
            if (PhotonNetwork.isMasterClient)
                MasterLoadedGame(); //for the player who create the room
            else
                NonMasterLoadedGame(); //for other player
        }
    }

    //Load game by player who created the room
    private void MasterLoadedGame()
    {
        PlayersInGame = 1;
        PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    //Load game by other player
    private void NonMasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
    }

    /*//Called when any player in the room disconnected from the photon server
    private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer)
    {
        this.DisconnectedPanel.gameObject.SetActive(true);
    }*/

    //Synchronize next
    public void OnClickNext()
    {
        PhotonView.RPC("RPC_Next", PhotonTargets.All);
    }

    //Run if back to main menu button clicked
    public void OnClickLobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    //Send player 1 id
    public void Send_idA(string idA)
    {
        PhotonView.RPC("RPC_SendidA", PhotonTargets.All, idA);
    }

    //Send player 2 id
    public void Send_idB(string idB)
    {
        PhotonView.RPC("RPC_SendidB", PhotonTargets.All, idB);
    }

    //Send multiplay id
    public void Send_MultiplayId(string multiplayid)
    {
        PhotonView.RPC("RPC_SendMultiplayId", PhotonTargets.All, multiplayid);
        Debug.Log("Multiplay ID in Send_MultiplayId: " + multiplayid);
    }

    //Synchronize action associated to the choice chosen by the player
    public void Sync_ChoiceAction(string action)
    {
        PhotonView.RPC("RPC_ChoiceAction", PhotonTargets.All, action);
    }

    public void ChatNotification()
    {
        if(PhotonNetwork.player.ID == 1)
            PhotonView.RPC("RPC_ChatNotif", PhotonTargets.Others);
        else
            PhotonView.RPC("RPC_ChatNotif", PhotonTargets.MasterClient);
    }

    //Called when the player failed to connect to photon server
    private void OnDisconnected(object[] codeAndMessage)
    {
        Debug.Log("OnDisconnected. StatusCode: " + codeAndMessage[1] + " ServerAddress: " + PhotonNetwork.ServerAddress);
        //Try to connect again
        PhotonNetwork.ConnectUsingSettings("0");
    }

    //All RPC
    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(6);
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        PlayersInGame++;
        if (PlayersInGame == PhotonNetwork.playerList.Length)
        {
            print("All players are in the game scene.");
        }
    }

    [PunRPC]
    private void RPC_Next()
    {
        NovelController.instance.Next();
    }

    [PunRPC]
    private void RPC_MakeChoice(ChoiceButton button)
    {
        ChoiceScreen.instance.MakeChoice(button);
    }

    [PunRPC]
    private void RPC_SendidA(string idA)
    {
        NovelController.instance.SendID_A(idA);
    }

    [PunRPC]
    private void RPC_SendidB(string idB)
    {
        NovelController.instance.SendID_B(idB);
    }

    [PunRPC]
    private void RPC_SendMultiplayId(string multiplayid)
    {
        NovelController.instance.SendMultiplayID(multiplayid);
        Debug.Log("Multiplay ID in RPC_SendMultiplayID: " + multiplayid);
    }

    [PunRPC]
    private void RPC_ChoiceAction(string action)
    {
        NovelController.instance.HandleLine(action);
    }

    [PunRPC]
    private void RPC_ChatNotif()
    {
        NovelController.instance.ActivateChatNotif();
    }
}