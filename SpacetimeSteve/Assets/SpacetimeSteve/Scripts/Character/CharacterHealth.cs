using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour {

    public bool isBlocking = false;

    public float maxHealth;
    public float currentHealth;
    public GameObject hitParticleEffect;

    public UISlider healthSlider;

    void Start()
    {
        healthSlider = GameObject.Find("HealthSlider").GetComponent<UISlider>();
    }

    public void HealPlayer(int healAmount)
    {
        if ((healAmount + currentHealth) >= maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += healAmount;
    }

    public void DamagePlayer(int initalDamage)
    {
        float damage = initalDamage;

        if(isBlocking)
            damage -= (initalDamage * 0.7f);

        Debug.Log("Isblocking: " + isBlocking + " Damage: " + damage);

        currentHealth -= damage;
        
        CheckifDead();
    }
	
	// Update is called once per frame
	void Update () {
        healthSlider.sliderValue = currentHealth / maxHealth;
	}

    void CheckifDead()
    {
        if (currentHealth <= 0)
        {
            PlayerDeath();
        }
        else
            GameObject.Instantiate(hitParticleEffect, transform.position + new Vector3(0, 1.4f, 0), transform.rotation);
    }


    void PlayerDeath()
    {
        healthSlider.sliderValue = 0;
        GameObject.Find("GameManager").GetComponent<GameManager>().EndGame();

        CharacterWeaponManager weaponManager = GetComponent<CharacterWeaponManager>();
        weaponManager.ClearWeapons();

        Destroy(gameObject);
    }
}
