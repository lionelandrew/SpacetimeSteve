using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public enum AmmoType
    {
        melee,
        projectile,
        bullet
    }

    public int weaponID;
    public AmmoType ammoType;
    public Texture2D weaponTexture;
    public int damage = 5;
    public int weaponAttackDistance = 3;
    public GameObject character;
    public float attackRate = 2;
    public float cooldownTimer = 0.0f;
    public bool hasAttacked = false;

    public virtual Weapon EquipWeapon(GameObject newCharacter)
    {
        character = newCharacter;
        transform.parent = character.transform;
        transform.localPosition = Vector3.zero + (Vector3.right * 0.8f);
        transform.rotation = character.transform.rotation;

        return this;
    }

    public virtual void WeaponUpdate()
    {

    }

    void Update()
    {
        WeaponUpdate();

        if (hasAttacked)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= attackRate)
            {
                cooldownTimer = 0.0f;
                hasAttacked = false;
            }
        }
    }

    public void Attack(){
        if (hasAttacked)
            return;

        if (character.tag == "Enemy")
        {
            EnemyAttack();
        }
        else if (character.tag == "Player")
        {
            CharacterAttack();
        }

        hasAttacked = true;
    }

    public virtual void CharacterAttack(){}

    public virtual void EnemyAttack(){}

    public bool CanAttack()
    {
        if (hasAttacked)
            return false;
        else
            return true;
    }

    public virtual Weapon Unequip() {

        character = null;
        transform.parent = null;
        transform.position = new Vector3(0, -40, 0);

        return this;

    }
}
