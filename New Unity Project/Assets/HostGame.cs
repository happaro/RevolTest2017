using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour
{
    [SerializeField]
    private uint roomSize = 2;
    private string roomName = "Fight";
    private UnityEngine.Networking.NetworkManager networkManager;

    void Start()
    {
        networkManager = UnityEngine.Networking.NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
    }

    public void CreateRoom()
    {
        networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
    }
}
