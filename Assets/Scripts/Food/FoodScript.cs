using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public int healAmount = 10;
    public AudioClip audioSound;

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.name == "Player"){
            Debug.Log("Player NEARBY");
            GameObject player = col.gameObject.transform.parent.gameObject;
            player.GetComponent<PlayerHealth>().gainHealth(healAmount);
            player.GetComponent<PlayerMovement>().playSound(audioSound);
            Destroy(this.gameObject);
        }
    }
}
