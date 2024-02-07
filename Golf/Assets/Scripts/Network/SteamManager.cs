using UnityEngine;
using UnityEngine.Events;

public class SteamManager : MonoBehaviour
{
    public uint appId;
    public UnityEvent OnSteamFailed;

    //Dont think we need this script?
    private void Awake()
    {
        DontDestroyOnLoad(this);
        try
        {
            Steamworks.SteamClient.Init(appId, true);
            Debug.Log("Steam is up and Running!");
        }
        catch (System.Exception e)
        {
            OnSteamFailed.Invoke();
            Debug.Log("<color=red>" + e.Message + "</color");
        }
    }
    private void OnApplicationQuit()
    {
        try
        {
            Steamworks.SteamClient.Shutdown();
        }catch (System.Exception e)
        {
            Debug.Log("<color=red>" + e.Message + "</color");
        }
    }
}
