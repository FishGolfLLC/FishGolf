using MPUIKIT;
using Steamworks.Data;
using TMPro;
using UnityEngine;

public class LobbyButtonData : MonoBehaviour
{
    public TextMeshProUGUI hostName;
    public TextMeshProUGUI playerCount;
    public TextMeshProUGUI serverRegion;
    public MPImage locked;
    [HideInInspector] public Lobby lobby;

    public async void JoinLobbyAsync()
    {
        Debug.Log("Joining lobby: " + hostName.text);
        await lobby.Join();
    }
}
