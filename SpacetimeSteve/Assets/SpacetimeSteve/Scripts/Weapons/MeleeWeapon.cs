using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon
{
    public GameObject slashParticleEffect;
    public Transform aimTarget;

    public override Weapon EquipWeapon(GameObject newCharacter)
    {
        Weapon newWeapon = base.EquipWeapon(newCharacter);

        if (character.tag == "Player")
        {
            aimTarget = GameObject.Find("AimTarget").transform;
        }

        return newWeapon;
    }


    public override void CharacterAttack()
    {
        RaycastHit hit;

        Debug.DrawRay(character.transform.position, aimTarget.position - character.transform.position, Color.white, 1.0f);

        if (Physics.Raycast(character.transform.position, aimTarget.position - character.transform.position, out hit))
        {
            if (hit.collider.tag == "Enemy" && hit.distance <= weaponAttackDistance)
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damage);
            }
        }

        ShowAttackEffect(character.transform.TransformDirection(Vector3.forward));
    }

    public override void EnemyAttack()
    {
        RaycastHit hit;

        Debug.DrawRay(character.transform.position, character.transform.TransformDirection(Vector3.forward), Color.white, 1.0f);

        if (Physics.Raycast(character.transform.position, character.transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.collider.tag == "Player" && hit.distance <= weaponAttackDistance)
            {
                CharacterHealth charHealth = hit.collider.GetComponent<CharacterHealth>();
                charHealth.DamagePlayer(damage);
            }
        }

        ShowAttackEffect(character.transform.TransformDirection(Vector3.forward));
    }

    void ShowAttackEffect(Vector3 spawnPosition)
    {
        GameObject.Instantiate(slashParticleEffect, character.transform.position + character.transform.TransformDirection(Vector3.forward), transform.rotation);
    }
}