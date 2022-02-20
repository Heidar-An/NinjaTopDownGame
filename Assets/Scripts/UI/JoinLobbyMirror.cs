using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMirror : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;
    [SerializeField] private TMP_InputField ipAddressInputField = null;


    // Start is called before the first frame update
    public void JoinLobby()
    {
        string ipAddress = ipAddressInputField.text;

        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();
    }
}
