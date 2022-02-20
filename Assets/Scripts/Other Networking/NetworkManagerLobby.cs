using UnityEngine;
using Mirror;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NetworkManagerLobby : NetworkManager
{
    List<GameObject> playerPrefabs = new List<GameObject>();
    public override void OnStartServer(){
        spawnPrefabs = Resources.LoadAll<GameObject>("Networking/Prefabs/PlayerPrefabs").ToList();
        Debug.Log(spawnPrefabs.Count);
        Debug.Log(spawnPrefabs[0]);
        playerPrefabs = spawnPrefabs;
    } 
    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("Networking/Prefabs/PlayerPrefabs");
        // playerPrefabs = spawnPrefabs;
        foreach (var prefab in spawnablePrefabs)
        {
            NetworkClient.RegisterPrefab(prefab);
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Debug.Log("SERVER ADDING PLAYER");
        if (SceneManager.GetActiveScene().name == "FirstLevel"){
            Debug.Log("PLAYER IS BEING ADDDED");
            int index = 0;
            GameObject player = Instantiate(playerPrefabs[index]);
            NetworkServer.AddPlayerForConnection(conn, player);
        }else{
            base.OnServerAddPlayer(conn);
        }  
    }

    public override void ServerChangeScene(string newSceneName){
        if(SceneManager.GetActiveScene().name == "MainMenu"){
            base.ServerChangeScene(newSceneName);
        }
    }
        

}
