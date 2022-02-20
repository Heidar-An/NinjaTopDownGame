using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesButton : MonoBehaviour
{
    public AudioClip clip;
    public SoundManager manager;
    public bool yesButton;
    public GameObject mainMenu;
    public GameObject LobbySystem;

    public void OnClick(){
        manager.PlaySound(clip);
        if(yesButton == false){
            Application.Quit();
        }
    }
}
