using UnityEngine;
using System.Collections;
using System.Timers;

public class NetworkManager : MonoBehaviour {

    private const string typeName = "ExpoDraw App";
    private const string gameName = "ExpoDrawRoom";
    private HostData[] hostList;
    public GameObject playerPrefab;

    //As host
    private void StartServer()
    {
        Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
    }

    void OnServerInitialized()
    {
        //SpawnPlayer();
        Debug.Log("Add ur vectorlines to server, ur a host harry");
    }

    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(0, 0, 30, 30), "Start Server"))
                StartServer();

            if (GUI.Button(new Rect(35, 0, 30, 30), "Refresh Hosts"))
                RefreshHostList();

            if (hostList != null)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(70, 0 + (30 * i), 30, 30), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
        }
    }

    //As client
    private void RefreshHostList()
    {
        MasterServer.RequestHostList(typeName);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
            hostList = MasterServer.PollHostList();
    }

    private void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
    }

    void OnConnectedToServer()
    {
        //SpawnPlayer();
        Debug.Log("Add ur vectorlines to server, ur a client harry");
    }

	// Use this for initialization
	void Start () {

	}

    void LateUpdate()
    {
        Connectshit();
    }

    private void Connectshit(){
        if (!Network.isClient && !Network.isServer)
        {
            RefreshHostList(); 
            if (hostList != null)
            {
                if (hostList.Length > 0)
                {
                    Debug.Log(hostList.Length);
                    
                }
            }
        }
    }

    private void SpawnPlayer()
    {
        Network.Instantiate(playerPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
    }
}
