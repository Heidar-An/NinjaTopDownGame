using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySound(AudioClip audioClip){
        audioSource.PlayOneShot(audioClip, 0.25f);
    }

    public IEnumerator waitSound(){
        yield return new WaitWhile (()=> audioSource.isPlaying);
    }
}
