using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Photon.Pun;

namespace AbhiTechGames.Networking
{
    public class MultiplayerManager : MonoBehaviourPunCallbacks
    {
        [Tooltip("Enter Name InputField Text")]
        [SerializeField] private TMP_InputField nameInputField;

        [Tooltip("Connect Button Text")]
        [SerializeField] private TMP_Text connectText;

        [Tooltip("UI Required to Connect to Server, name and connect button basically")]
        [SerializeField] private GameObject connectToServerUI;

        [Tooltip("UI Required to Create or Join Rooms in Server")]
        [SerializeField] private GameObject createOrJoinsRoomsUI;

        private string playerName;

        private void Start()
        {
            playerName = PlayerPrefs.GetString("PlayerName", "Enter the Name");

            if (playerName != "Enter the Name") // Player Name Exits
                nameInputField.text = playerName;
        }

        public void ConnectedToServer()
        {
            /// <summary>
            /// This Function called by a Button "CONNECT" (to Server)
            /// Check for a Valid Name,
            /// Change Button Connect Text to Connecting,
            /// Connects to Server.
            /// </summary>

            if (nameInputField.text.Length >= 1) // Name Exist
            {
                playerName = nameInputField.text;
                PlayerPrefs.SetString("PlayerName", playerName); // Saving Player Name
                connectText.text = "CONNECTING...";

                PhotonNetwork.NickName = playerName; // Change Player Name is Network
                PhotonNetwork.AutomaticallySyncScene = true; //Sync all player Scenes

                PhotonNetwork.ConnectUsingSettings(); // Connects Player to Photon Server
            }
        }

        public override void OnConnectedToMaster()
        {
            /// <summary>
            /// We are Connected to Photon Master Server
            /// Now we can Create or Join Rooms
            /// </summary>

            connectToServerUI.SetActive(false);
            createOrJoinsRoomsUI.SetActive(true);

            GetComponent<LobbyManager>().enabled = true;
        }
    }
}
