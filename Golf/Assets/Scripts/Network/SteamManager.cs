using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SteamManager : MonoBehaviour
{
    public uint appId;
    public UnityEvent OnSteamFailed;

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
