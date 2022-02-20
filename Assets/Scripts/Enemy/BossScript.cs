using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float timeTillAttack = 3f;
    public float attackTimePassed = 0f;
    public GameObject attackOne;
    public GameObject attackTwo;
    public GameObject Player;
    public int health = 100;
    public Animator animator;
    public List<GameObject> firePoints = new List<GameObject>();
    
    int attackAmount = 0;

    // Update is called once per frame
    void Update()
    {
        
        attackTimePassed += Time.deltaTime;
        if(attackTimePassed >= timeTillAttack){
            attackTimePassed = 0f;
            Vector2 direction = Player.transform.position - transform.position;
            GameObject projectile = null;
            Vector3 proPosition = transform.position;
            float lowDistance = Vector3.Distance(proPosition, Player.transform.position);
            foreach(GameObject firePoint in firePoints){
                if(Vector3.Distance(Player.transform.position, firePoint.transform.position) < lowDistance){
                    proPosition = firePoint.transform.position;
                }
            }
            if(attackAmount % 3 == 0){
                projectile = Instantiate(attackTwo, proPosition, Quaternion.identity);
                attackAmount += 1;
            }else{
                attackAmount += 1;
                projectile = Instantiate(attackOne, proPosition, Quaternion.identity);
            }
            projectile.GetComponent<AttackProjectileScript>().setDirection(direction);
        }
    }

    public void hitFinished(){
        Debug.Log("Function recieved");
    }

    public void takeDamage(int damage){
        health -= damage;
        animator.SetBool("HitTaken", true);
    }

    void checkHealth(){
        if(health <= 100){
            Destroy(this.gameObject);
        }
    }
}
