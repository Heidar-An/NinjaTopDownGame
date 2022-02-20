using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectWeapon : MonoBehaviour
{
    public TMP_Text weaponText;
    public GameObject weaponChild;
    public Sprite weapon;
    public int damage = 30;
    public RuntimeAnimatorController weaponController;

    bool playerNearby;
    GameObject player = null;

    void Update(){
        if(playerNearby){
            if(Vector3.Distance(transform.position, player.transform.position) < 1.5f){
                weaponText.gameObject.SetActive(true);
                weaponText.text = "Press R to pick up weapon";
                if(Input.GetKeyDown(KeyCode.R)){
                    Debug.Log("Weapon Picked Up");
                    if(player != null){
                        player.GetComponent<PlayerMovement>().setWeapon(weapon);
                        player.GetComponent<PlayerMovement>().setDamage(damage);
                        // set animator controller
                        player.GetComponent<PlayerMovement>().setAnimatorController(weaponController);
                        weaponText.gameObject.SetActive(false);
                        

                        Destroy(gameObject);
                    }
                    
                }
            }
            else{
                weaponText.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.name == "Player"){
            Debug.Log("Player NEARBY");
            player = col.gameObject.transform.parent.gameObject;
            playerNearby = true;
        }
    }
    
}
