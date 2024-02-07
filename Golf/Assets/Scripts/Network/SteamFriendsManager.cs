using Steamworks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SteamFriendsManager : MonoBehaviour
{
    public RawImage profilePic;
    public string playerName;
    public List<RawImage> friendsListImages;
    [SerializeField] Transform canvas;
    // Start is called before the first frame update
    async void Start()
    {
        if (!SteamClient.IsValid) return;

        await GetLocalPlayerData();
        await GetFriends();
    }

    /// <summary>
    /// Grab the friends list of the local player
    /// </summary>
    /// <returns></returns>
    public async Task GetFriends()
    {
        IEnumerable<Friend> friends = SteamFriends.GetFriends();
        foreach (var friend in friends)
        {
            var img = await GetAvatar(friend.Id);
            RawImage x = Instantiate(profilePic, canvas);
            x.texture = img.Value.GetTextureFromImage();
            friendsListImages.Add(x);
        }
    }
    /// <summary>
    /// Grabs the Profile Picture and Name of the local player
    /// </summary>
    /// <returns></returns>
    public async Task GetLocalPlayerData()
    {
        playerName = SteamClient.Name;
        var img = await GetAvatar(SteamClient.SteamId);
        profilePic.texture = img.Value.GetTextureFromImage();
    }
    private static async Task<Steamworks.Data.Image?> GetAvatar(SteamId id)
    {
        try
        {
            // Get Avatar using await
            return await SteamFriends.GetLargeAvatarAsync(id);
        }
        catch (Exception e)
        {
            // If something goes wrong, log it
            Debug.Log("<color=red>" + e + "</color>");
            return null;
        }
    }
}
