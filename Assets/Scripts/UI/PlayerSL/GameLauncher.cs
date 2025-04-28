using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameLauncher : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button startButton;
    [SerializeField] private string gameSceneName = "mapa"; // Nombre exacto de tu escena de mapa

    private void Start()
    {
        // Aseg�rate de que solo el MasterClient vea y pueda pulsar este bot�n
        startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        startButton.onClick.AddListener(OnClickStartGame);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        // Si cambia el MasterClient (porque el original se desconect�), actualiza la visibilidad
        startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }

    private void OnClickStartGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        // Opcional: cerrar el lobby para que no entren m�s jugadores
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        // Esta llamada har� que todos los clientes carguen la escena indicada
        PhotonNetwork.LoadLevel(gameSceneName);
    }
}
