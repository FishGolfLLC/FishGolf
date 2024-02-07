using Netcode.Transports.Facepunch;
using Steamworks;
using Steamworks.Data;
using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class SteamLobbyManager : MonoBehaviour
{
    public static SteamLobbyManager instance;
    [SerializeField] TMP_InputField lobbyIDinput;//This will be replaced as well
    [SerializeField] TextMeshProUGUI lobbyID; //this will get replaced by an event that fires so the UI can update

    Lobby currentLobby;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);
        DontDestroyOnLoad(this);
    }
    private void OnEnable()
    {
        SteamMatchmaking.OnLobbyCreated += LobbyCreated;
        SteamMatchmaking.OnLobbyEntered += LobbyEntered;
        SteamFriends.OnGameLobbyJoinRequested += GameLobbyJoinRequested;
        ShowLobbies();
    }

    private async void GameLobbyJoinRequested(Lobby lobby, SteamId arg2)
    {
        await lobby.Join();
        Debug.Log("Joining...");
    }

    private void LobbyEntered(Lobby obj)
    {
        currentLobby = obj;
        lobbyID.text = obj.Id.ToString();

        //Dont want host doing anything else
        if (NetworkManager.Singleton.IsHost) return;

        NetworkManager.Singleton.gameObject.GetComponent<FacepunchTransport>().targetSteamId = obj.Owner.Id;
        NetworkManager.Singleton.StartClient();
        Debug.Log("Lobby Entered.");
    }
    private async void ShowLobbies()
    {
        Lobby[] lobbies = await SteamMatchmaking.LobbyList.WithSlotsAvailable(1).RequestAsync();
        foreach(Lobby lob in lobbies)
        {
            Debug.Log("Lobby:" + lob.Id);
        }
        return;
    }

    private void LobbyCreated(Result arg1, Lobby arg2)
    {
        if(arg1 == Result.OK)
        {
            Debug.Log("Lobby Created.");
            NetworkManager.Singleton.StartHost();
            currentLobby.SetPublic();
            currentLobby.SetJoinable(true);
        }
    }

    private void OnDisable()
    {
        SteamMatchmaking.OnLobbyCreated -= LobbyCreated;
        SteamMatchmaking.OnLobbyEntered -= LobbyEntered;
        SteamFriends.OnGameLobbyJoinRequested -= GameLobbyJoinRequested;

    }
    public async void HostLobby()
    {
        await SteamMatchmaking.CreateLobbyAsync(4); //The number here is the amount of players in a lobby
        Debug.Log("Hosting...");
    }
    public async void JoinLobbyWithID(string id)
    {
        ulong Id;
        if (!ulong.TryParse(id, out Id))
            return;
        Lobby[] lobbies = await SteamMatchmaking.LobbyList.WithSlotsAvailable(1).RequestAsync();
        foreach (Lobby lobby in lobbies)
        {
            if(lobby.Id == Id)
            {
                await lobby.Join();
                Debug.Log("Joining lobby with " + Id);
                return;
            }
        }
    }
}
