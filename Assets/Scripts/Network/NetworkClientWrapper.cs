using System;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkManager))]
public class NetworkClientWrapper: MonoBehaviour
{
    public const string DefaultAddres = "localhost";
    public const int DefaultPort = 7777;
    public delegate void OnConnectedToServer(NetworkMessage netMsg);
    public event OnConnectedToServer OnConnectionEstablished;
    public NetworkManager NetworkManager;

    void Start()
    {
        NetworkAddress = DefaultAddres;
        NetworkPort = DefaultPort;
    }

    public string NetworkAddress
    {
        get
        {
            return NetworkManager.networkAddress;
        }
        set
        {
            NetworkManager.networkAddress = value;
        }
    }

    public int NetworkPort
    {
        get
        {
            return NetworkManager.networkPort;
        }
        set
        {
            NetworkManager.networkPort = value;
        }
    }

    public void StartServerOnly()
    {
        NetworkManager.StartServer();
    }

    public void StartHost()
    {
        NetworkManager.StartHost();
        NetworkManager.client.RegisterHandler(MsgType.Connect, OnConnected);
    }

    public void StartClient()
    {
        NetworkManager.StartClient();
        NetworkManager.client.RegisterHandler(MsgType.Connect, OnConnected);
    }

    public void StopConnection()
    {
        if (NetworkServer.active && NetworkClient.active)
        {
            NetworkManager.StopHost();
        }
    }

    public void LoadRescuersWon()
    {
        NetworkManager.ServerChangeScene("RescuersWon");
    }
    
    public void LoadSuicidersWon()
    {
        NetworkManager.ServerChangeScene("SuicidersWon");
    }

    private void ConnectPlayer()
    {
        if (NetworkClient.active && !ClientScene.ready)
        {
            ClientScene.Ready(NetworkManager.client.connection);

            if (ClientScene.localPlayers.Count == 0)
            {
                ClientScene.AddPlayer(0);
            }
        }
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        ConnectPlayer();
        OnConnectionEstablished(netMsg);
        Debug.Log("lol");
    }
}
