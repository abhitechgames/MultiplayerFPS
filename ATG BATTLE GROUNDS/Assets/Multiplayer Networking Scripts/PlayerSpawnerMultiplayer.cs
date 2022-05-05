using UnityEngine;
using Photon.Pun;

public class PlayerSpawnerMultiplayer : MonoBehaviour
{
    [Tooltip("Player Prefab 1 to Spawn")]
    [SerializeField] private GameObject playerPrefab1;

    [Tooltip("Player Prefab 2 to Spawn")]
    [SerializeField] private GameObject playerPrefab2;

    [Tooltip("Spawn Positon for Player")]
    [SerializeField] private Transform[] spawnPos;

    private void Start()
    {
        if(PhotonNetwork.PlayerList[0] == PhotonNetwork.LocalPlayer)
            PhotonNetwork.Instantiate(playerPrefab1.name, spawnPos[0].position, Quaternion.identity);
        else
            PhotonNetwork.Instantiate(playerPrefab2.name, spawnPos[1].position, Quaternion.identity);

    }
}
