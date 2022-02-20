using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectileScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 3f;
    public int damage = 5;
    public Animator animator;
    public AudioClip sound;

    Vector2 movement;

    public void setDirection(Vector2 movementDirection){
        movement = movementDirection;
        movement.Normalize();
    }

    void Start(){
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D col){
        animator.SetBool("PlayerHit", true);
        if(col.gameObject.name == "Player"){
            GameObject player = col.gameObject.transform.parent.gameObject;
            player.GetComponent<PlayerMovement>().playSound(sound);
            player.GetComponent<PlayerHealth>().dealDamage(damage);
        }
    }
}
