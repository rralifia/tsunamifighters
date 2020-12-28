using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _roomNameText;
    private TextMeshProUGUI RoomNameText
    {
        get { return _roomNameText; }
    }

    public string RoomName { get; private set; }

    public bool Updated { get; set; }

    void Start()
    {
        GameObject lobbyCanvasObj = MainCanvasManager.instance.LobbyCanvas.gameObject;
        if (lobbyCanvasObj == null)
            return;

        LobbyCanvas lobbyCanvas = lobbyCanvasObj.GetComponent<LobbyCanvas>();

        //To make the button in roomlist will join the player to the room based on roomname shown in the button when clicked
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => lobbyCanvas.OnClickJoinRoom(RoomNameText.text));
    }
    
    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        //remove the listener if button destroyed
        button.onClick.RemoveAllListeners();
    }

    //Set roomname to the roomnametext in the roomlistingprefab
    public void SetRoomNameText(string text)
    {
        RoomName = text;
        RoomNameText.text = RoomName;
    }
}
