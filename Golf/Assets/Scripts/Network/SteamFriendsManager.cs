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
            RawImage x = Instantiate(profilePic);
            x.texture = GetTextureFromImage(img.Value);
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
        profilePic.texture = GetTextureFromImage(img.Value);
    }

    //Convert this to an extension of Texture2D
    public static Texture2D GetTextureFromImage(Steamworks.Data.Image image)
    {
        // Create a new Texture2D
        var avatar = new Texture2D((int)image.Width, (int)image.Height, TextureFormat.ARGB32, false);

        // Set filter type, or else its really blury
        avatar.filterMode = FilterMode.Trilinear;

        // Flip image
        for (int x = 0; x < image.Width; x++)
        {
            for (int y = 0; y < image.Height; y++)
            {
                var p = image.GetPixel(x, y);
                avatar.SetPixel(x, (int)image.Height - y, new UnityEngine.Color(p.r / 255.0f, p.g / 255.0f, p.b / 255.0f, p.a / 255.0f));
            }
        }

        avatar.Apply();
        return avatar;
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
