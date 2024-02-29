using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class Extensions
{
    public static Texture2D GetTextureFromImage(this Steamworks.Data.Image image)
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
    public static void DestroyChildren(this GameObject t)
    {
        t.transform.Cast<Transform>().ToList().ForEach(c => Object.Destroy(c.gameObject));
    }
    public static void DestroyChildren(this Transform t)
    {
        DestroyChildren(t.gameObject);
    }
}
