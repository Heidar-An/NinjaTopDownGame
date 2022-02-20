using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterSelectMirror : NetworkBehaviour
{
    [SerializeField] private List<GameObject> characterInstances = new List<GameObject>();
    [SerializeField] private NetworkManagerLobby networkManager;

    private int CharacterIndex = 0;

    public AudioClip clip;
    public SoundManager manager;

    public void OnClick(){
        manager.PlaySound(clip);
        manager.waitSound();
        networkManager.ServerChangeScene("FirstLevel");
        this.Select();
    }

    public void setIndex(int index){
        CharacterIndex = index;
    }

    public void Select(){
        CmdSelect(CharacterIndex);
    }

    [Command(requiresAuthority = false)]
    public void CmdSelect(int characterIndex, NetworkConnectionToClient sender = null)
    {
        Debug.Log("PLAYER SELECTED");
        sender.authenticationData = CharacterIndex;
        GameObject characterInstance = Instantiate(characterInstances[characterIndex]);
        NetworkServer.Spawn(characterInstance, sender);
    }
}
