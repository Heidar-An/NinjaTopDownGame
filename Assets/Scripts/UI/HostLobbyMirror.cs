using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostLobbyMirror : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;
    
    public void HostLobby(){
        networkManager.StartHost();
    }
}
