using UnityEngine;
using System.Collections;
using System;

public class EnemyHealth : MonoBehaviour {

    public ItemGetter gameItemGetter;
    public GameObject deathExplosion;
    public GameObject hitParticleEffect;
    public int enemyHealth = 20;

    public void TakeDamage(int initialDamage)
    {
        enemyHealth -= initialDamage;
        
        CheckIfDead();
    }

    public void CheckIfDead()
    {
        if (enemyHealth <= 0)
        {
            Death();
        }
        else
            GameObject.Instantiate(hitParticleEffect, transform.position + new Vector3(0, 1.4f, 0), transform.rotation);
    }

    public void Death()
    {
        GameObject.Find("WaveManager").GetComponent<WaveManager>().RemoveEnemy(gameObject);
        PlayerScore.AddPlayerScore(150);
        GameObject.Instantiate(deathExplosion, transform.position, transform.rotation);
        SocialplayDropItem();
        Destroy(gameObject);
    }

    void SocialplayDropItem()
    {
        if (gameItemGetter != null)
            gameItemGetter.GetItems();
    }
}
