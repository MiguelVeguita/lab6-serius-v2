using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using TMPro;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [Header("Prefabs de jugador (en el mismo orden que CharacterStats)")]
    [SerializeField] private GameObject[] playerPrefabs;

    [Header("Puntos de Spawn")]
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        // Obtén el índice de personaje guardado en las custom properties
        object idx;
        PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("characterIndex", out idx);
        int characterIndex = idx != null ? (int)idx : 0;

        // Escoge un punto de spawn (puedes hacer round-robin, random, etc.)
        Transform spawn = spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber % spawnPoints.Length];

        // Instancia la versión de red del prefab correspondiente
        GameObject player = PhotonNetwork.Instantiate(
            playerPrefabs[characterIndex].name,
            spawn.position,
            spawn.rotation);

        // Opcional: si tu prefab tiene un componente que muestre el nombre,
        // puedes inicializarlo aquí:
        var nametag = player.GetComponentInChildren<TextMeshProUGUI>();
        if (nametag != null)
            nametag.text = PhotonNetwork.NickName;
    }
}
