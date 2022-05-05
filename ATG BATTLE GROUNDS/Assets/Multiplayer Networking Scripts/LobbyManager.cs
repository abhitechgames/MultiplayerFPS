using System.Collections;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Tooltip("Name of the Room Player Assigned")]
    [SerializeField] private TMP_InputField roomNameToCreate;

    [Tooltip("Name of the Room Player want to Join")]
    [SerializeField] private TMP_InputField roomNameToJoin;

    [Tooltip("Room Name Text, to changed the room name when player joins a Room")]
    [SerializeField] private TMP_Text joinedRoomName;

    [Tooltip("Create Room Text, to change Text to Creating while creating the Room")]
    [SerializeField] private TMP_Text createRoomButtonText;

    [Tooltip("Creata and Join Pannel, deactivates when player joins a Room")]
    [SerializeField] private GameObject lobbyPannel;

    [Tooltip("Room Pannel, activates when player joins a Room")]
    [SerializeField] private GameObject roomPannel;

    [Tooltip("Room Pannel, activates when player joins a Room")]
    private string nameOfRoom;

    [Tooltip("Player 1 text component")]
    [SerializeField] private TMP_Text player1;

    [Tooltip("Player 2 text component")]
    [SerializeField] private TMP_Text player2;

    [Tooltip("Waiting for 2nd Player or Entering the Match Text")]
    [SerializeField] private TMP_Text enteringTheMatchText;

    void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnRoomCreateClick()
    {
        /// <summary>
        /// Room Creation Process : 
        /// Check for a Valid Room Name
        /// Create a Room using that Name and assign Max player using new RoomOptions()
        /// change create room text to CREATING...
        /// </summary>

        nameOfRoom = roomNameToCreate.text;

        if (nameOfRoom.Length >= 1)
        {
            Debug.Log("Creating Room");

            PhotonNetwork.CreateRoom(nameOfRoom.ToUpper(), new RoomOptions() { MaxPlayers = 2 });
            createRoomButtonText.text = "CREATING...";
        }
    }

    public void MakeAMatch()
    {
        /// <summary>
        /// Join a Random Room
        /// </summary>

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby Joined");
    }

    public override void OnJoinedRoom()
    {
        /// <summary>
        /// Joined Room Process : 
        /// Change Room Name Text
        /// Disable Create or Join Room Option
        /// Enable Joined Room Pannel
        /// </summary>

        joinedRoomName.text = PhotonNetwork.CurrentRoom.Name;

        Debug.Log("Room Created");

        lobbyPannel.SetActive(false);
        roomPannel.SetActive(true);

        UpdatePlayerList();
    }

    public void LeaveARoom()
    {
        /// <summary>
        /// Leaving a Joined Room in Photon, Called by a Button
        /// </summary>

        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        /// <summary>
        /// Called by Photon Network
        /// Deactivate the Room Pannel
        /// Activate the Lobby Pannel
        /// </summary>

        roomPannel.SetActive(false);
        lobbyPannel.SetActive(true);
    }

    public void SearchAndJoinRooms()
    {
        if (roomNameToJoin.text.Length >= 1)
        {
            PhotonNetwork.JoinRoom(roomNameToJoin.text.ToUpper());
        }
    }

    public override void OnConnectedToMaster()
    {
        /// <summary>
        /// When we Joined the Photon Network it will automatically joins the Lobby
        /// </summary>

        PhotonNetwork.JoinLobby();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        /// <summary>
        /// Called when Player enter a Room
        /// Updates Player List
        /// </summary>
        UpdatePlayerList();
    }

    public void UpdatePlayerList()
    {
        /// <summary>
        /// Updates Player 1 and Player 2 Name according to No. of players join...
        /// if There are 2 players then start the match
        /// </summary>

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            player1.text = PhotonNetwork.PlayerList[0].NickName;
        else if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            player1.text = PhotonNetwork.PlayerList[0].NickName;
            player2.text = PhotonNetwork.PlayerList[1].NickName;

            StartCoroutine(StartMatch());
        }
    }

    IEnumerator StartMatch()
    {
        /// <summary>
        /// Handle Start Match Text
        /// and Starts the Match
        /// </summary>

        enteringTheMatchText.text = "Entering the Match";
       
        yield return new WaitForSeconds(6f);
        
        //Start Match Code
        PhotonNetwork.LoadLevel("2-Multiplayer Ground");
    }
}
