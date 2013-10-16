using UnityEngine;
using System.Collections;

public class ProjectileWeapon : Weapon {

    public GameObject bulletPrefab;
    Transform aimTarget;

    public int currentAmmo = 0;


    public override Weapon EquipWeapon(GameObject newCharacter)
    {
        Weapon newWeapon = base.EquipWeapon(newCharacter);

        currentAmmo = CharacterWeaponManager.ProjectileAmmo;
        if (aimTarget == null && character != null && character.tag == "Player")
            aimTarget = GameObject.Find("AimTarget").transform;

        return newWeapon;
    }

    public override void CharacterAttack()
    {
        if (CheckForAmmo() == false)
            return;

        Transform shootPoint = character.transform.FindChild("ShootPoint");

        GameObject bullet = (GameObject)GameObject.Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(aimTarget.position - shootPoint.position));

        Projectile projectile = bullet.AddComponent<Projectile>();
        projectile.isPlayer = true;
        projectile.damageAmount = damage;

        Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();

        bulletRigidBody.AddForce(bulletRigidBody.transform.forward * 1400);
        bulletRigidBody.AddTorque(Random.Range(2, 5), Random.Range(2, 5), Random.Range(2, 5));

    }

    public override void EnemyAttack()
    {
        GameObject bullet = (GameObject)GameObject.Instantiate(bulletPrefab, character.transform.position + character.transform.TransformDirection(Vector3.forward), Quaternion.LookRotation(character.transform.TransformDirection(Vector3.forward + new Vector3(0,0.1f,0))));

        Projectile projectile = bullet.AddComponent<Projectile>();
        projectile.isPlayer = false;
        projectile.damageAmount = damage;

        Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();

        bulletRigidBody.AddForce(bulletRigidBody.transform.forward * 1400);
        bulletRigidBody.AddTorque(Random.Range(2, 5), Random.Range(2, 5), Random.Range(2, 5));
    }

    public bool CheckForAmmo()
    {
        currentAmmo = CharacterWeaponManager.ProjectileAmmo;

        if (currentAmmo > 0)
        {
            currentAmmo--;
            CharacterWeaponManager.ProjectileAmmo--;
            return true;
        }
        else
        {
            return false;
        }
    }
}
