using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int damage = 5;
    public int health = 50;
    bool movePlayer = false;
    GameObject player = null;

    // Update is called once per frame
    void Update()
    {
        if(movePlayer){
            if(player != null){
                float step = (player.GetComponent<PlayerMovement>().getSpeed() / 2) * Time.deltaTime;
                if(Vector3.Distance(transform.position, player.transform.position) > 0.5f){
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        Debug.Log(col.gameObject.name);
        if(col.gameObject.name == "Player"){
            movePlayer = true;
            player = col.gameObject.transform.parent.gameObject;
        }
        GetComponent<BoxCollider2D>().isTrigger = false;
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.name == "PlayerParent"){
            Debug.Log("PLAYER PARENT HIT");
            col.gameObject.GetComponent<PlayerHealth>().dealDamage(damage);
        }
    }

    public void takeDamage(int damage){
        health -= damage;
        checkHealth();
    }

    void checkHealth(){
        if(health <= 0){
            Destroy(this.gameObject);
        }
    }
}
