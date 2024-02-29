using Steamworks;
using Steamworks.Data;
using UnityEngine;

public class LobbyList : MonoBehaviour
{
    [SerializeField] LobbyButtonData lobbyObject;
    [SerializeField] Transform lobbyListParent;
    // Start is called before the first frame update
    void Start()
    {
        lobbyListParent.DestroyChildren();
    }
    private async void OnEnable()
    {
        Lobby[] lobbies = await SteamLobbyManager.GetLobbies();
        foreach(Lobby lobby in lobbies)
        {
            var lobbyButton = Instantiate(lobbyObject, lobbyListParent, false);
            string hostName = lobby.GetData(LobbyDataConstants.HostName);
            string serverRegion = lobby.GetData(LobbyDataConstants.ServerRegion);
            lobbyButton.hostName.text = hostName.Length == 0 ? "No Name" : hostName;
            lobbyButton.playerCount.text = lobby.MemberCount.ToString() + "/" + lobby.MaxMembers.ToString();
            lobbyButton.serverRegion.text = serverRegion.Length == 0 ? "Unknown Region" : serverRegion;
            lobbyButton.locked.color = lobby.MemberCount >= lobby.MaxMembers ? UnityEngine.Color.red : UnityEngine.Color.white;
        }
    }
}
