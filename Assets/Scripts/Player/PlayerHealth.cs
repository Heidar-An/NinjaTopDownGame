using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;
    public int health = 100;
    public int maxHealth = 100;
    public Sprite fullHeart;
    public Sprite threeQuartersHeart;
    public Sprite halfHeart;
    public Sprite quarterHeart;
    public Sprite emptyHeart;
    public List<Image> hearts = new List<Image>();

    const int MAX_FRAG = 20;
    List<Heart> heartFragments = new List<Heart>();

    void Start(){
        for (int i = 0; i < hearts.Count; i++){
            heartFragments.Add(new Heart(20));
        }
    }

    public void dealDamage(int damage){
        health -= damage;
        if(health < 0){
            health = 0;
        }
        checkHealth();
        DamageHearts(damage);
    }

    public void gainHealth(int healAmount){
        if(healAmount > maxHealth){
            health = maxHealth;
        }

        HealHearts(healAmount);
        
    }

    void checkHealth(){
        if(health <= 0){
            animator.SetBool("DeathCondition", true);
        }
    }

    void HealHearts(int healAmount){
        for(int i = 0; i < heartFragments.Count; i++){
            Heart heart = heartFragments[i];
            int missFragments = MAX_FRAG - heart.GetFragmentAmount();
            if(healAmount > missFragments){
                healAmount -= missFragments;
                heart.Heal(missFragments);
            }else{
                heart.Heal(healAmount);
                break;
            }
        }

        setHeartSprites();
    }

    void DamageHearts(int damageAmount){

        for (int i = hearts.Count - 1; i >= 0; i--){
            Heart singleHeart = heartFragments[i];
            if(damageAmount > singleHeart.GetFragmentAmount()){
                damageAmount -= singleHeart.GetFragmentAmount();
                singleHeart.Damage(singleHeart.GetFragmentAmount());
            }else{
                singleHeart.Damage(damageAmount);
                break;
            }
        }
        
        setHeartSprites();
    }

    void setHeartSprites(){
        for (int i = hearts.Count - 1; i >= 0; i--){
            Heart singleHeart = heartFragments[i];
            int fragmentAmount = singleHeart.GetFragmentAmount();

            if(fragmentAmount == 0){
                hearts[i].sprite = emptyHeart;
            }else if(fragmentAmount <= 5){
                hearts[i].sprite = quarterHeart;
            }else if(fragmentAmount <= 10){
                hearts[i].sprite = halfHeart;
            }else if(fragmentAmount <= 15){
                hearts[i].sprite = threeQuartersHeart;
            }else if(fragmentAmount <= 20){
                hearts[i].sprite = fullHeart;
            }
        }
    }

    public class Heart {
        private int fragments;

        public Heart(int fragments){
            this.fragments = fragments;
        }
        
        public int GetFragmentAmount(){
            return fragments;
        }

        public void SetFragments(int fragments){
            this.fragments = fragments;
        }

        public void Damage(int damageAmount){
            if(damageAmount >= fragments){
                fragments = 0;
            }else{
                fragments -= damageAmount;
            }
        }

        public void Heal(int healAmount){
            if(fragments + healAmount > MAX_FRAG){
                fragments = MAX_FRAG;
            }else{
                fragments += healAmount;
            }
        }
    }
}
