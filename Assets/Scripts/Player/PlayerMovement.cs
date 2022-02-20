using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public Animator weaponAnimator;
    public Rigidbody2D rb;
    public TMP_Text doorText;
    public GameObject childWeapon;
    public BoxCollider2D weaponBoxCollider;
    public SoundManager manager;

    Vector2 movement;
    GameObject[] doors;
    Sprite weapon;
    RuntimeAnimatorController weaponController;
    float spriteWidth;
    float spriteHeight;

    int damage = 20;

    void Start(){
        // find all doors
        doors = GameObject.FindGameObjectsWithTag("Doors");
    }

    void moveWeaponChild(){
        // move weapon attached to player (weapon might be empty)
        // Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
        Vector2 animatorMovement = new Vector2(animator.GetFloat("Horizontal"), animator.GetFloat("Vertical"));
        if(animatorMovement.x > 0){
            // childWeapon.transform.eulerAngles = new Vector3(0f, 0f, 90f);
            // childWeapon.transform.position = new Vector3(transform.position.x + 0.225f, transform.position.y - 0.372f, 0f);
        }else if(animatorMovement.x < 0){
            // childWeapon.transform.eulerAngles = new Vector3(0f, 0f, -90f);
            // childWeapon.transform.position = new Vector3(transform.position.x - 0.308f, transform.position.y - 0.375f, 0f);
        }else if(animatorMovement.y > 0){
            childWeapon.GetComponent<SpriteRenderer>().sprite = null;
        }else if(animatorMovement.y < 0){
            // childWeapon.transform.rotation = Quaternion.identity;
            // childWeapon.transform.position = new Vector3(transform.position.x + 0.31f, transform.position.y - 0.64f, 0f);
        }
    }

    public void setWeapon(Sprite weaponSprite){
        weapon = weaponSprite;
        childWeapon.GetComponent<SpriteRenderer>().sprite = weapon;
        spriteWidth = childWeapon.GetComponent<SpriteRenderer>().bounds.size.x;
        spriteHeight = childWeapon.GetComponent<SpriteRenderer>().bounds.size.y;
        weaponBoxCollider.size = new Vector2(spriteWidth, spriteHeight);
    }
    
    // Update is called once per frame
    void Update()
    {
        // Movement Input 
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // make sure that player is not dead
        if(animator.GetBool("DeathCondition") == false){
            if (movement != Vector2.zero){
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                childWeapon.GetComponent<SpriteRenderer>().sprite = null;
            }else{
                childWeapon.GetComponent<SpriteRenderer>().sprite = weapon;
                moveWeaponChild();
            }
        }
        
        
        // Attack Input
        float fight =  Input.GetAxisRaw("Attack");
        animator.SetBool("AttackCondition", false);
        if(weaponAnimator.runtimeAnimatorController != null){
            weaponAnimator.SetBool("AttackCondition", false);
        }
        

        if(fight != 0){
            Vector2 animatorMovement = new Vector2(animator.GetFloat("Horizontal"), animator.GetFloat("Vertical"));
            if(weaponAnimator.runtimeAnimatorController != null){
                weaponAnimator.SetFloat("Horizontal", animatorMovement.x);
                weaponAnimator.SetFloat("Vertical", animatorMovement.y);
                weaponAnimator.SetBool("AttackCondition", true);
            }
            
            animator.SetBool("AttackCondition", true);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);
        checkDoor();
    }

    public void setDamage(int newDamage){
        damage = newDamage;
    }

    public void setAnimatorController(RuntimeAnimatorController controller){
        weaponAnimator.runtimeAnimatorController = controller as RuntimeAnimatorController;
    }

    void checkDoor(){
        // Check if player is near a "door"
        foreach(GameObject door in doors){
            if(Vector3.Distance(transform.position, door.transform.position) < 1.5f){
                doorText.gameObject.SetActive(true);
                doorText.text = "Press Q to go inside the house";
                if(Input.GetKeyDown(KeyCode.Q)){
                    Debug.Log("NEW SCENE");
                }
            }else{
                doorText.gameObject.SetActive(false);
            }
        }
    }

    void FixedUpdate(){
        // Movement
        movement.Normalize();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // used for enemy speed
    public float getSpeed(){
        return moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D col){
        if(animator.GetBool("AttackCondition")){
            if(col.gameObject.tag == "Enemy"){
                GameObject enemy = col.gameObject;
                enemy.GetComponent<EnemyScript>().takeDamage(damage);
            }else if(col.gameObject.tag == "Boss"){
                Debug.Log("WORKING");
                GameObject boss = col.gameObject;
                boss.GetComponent<BossScript>().takeDamage(damage);
            }
        }
    }

    public void playSound(AudioClip sound){
        manager.PlaySound(sound);
    }
}
