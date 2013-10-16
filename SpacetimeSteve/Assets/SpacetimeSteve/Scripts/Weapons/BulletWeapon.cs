using UnityEngine;
using System.Collections;

public class BulletWeapon : Weapon {

    public Transform aimTarget;
    public GameObject bullet;

    public int bulletInClip;
    public int maxClipAmount;
    public int totalAmmo;

    public float reloadTime = 2.0f;
    public float timer = 0.0f;

    public bool isReloading = false;


    public override Weapon EquipWeapon(GameObject newCharacter)
    {
        Weapon newWeapon = base.EquipWeapon(newCharacter);

        if (character.tag == "Player")
        {
            aimTarget = GameObject.Find("AimTarget").transform;
        }

        return this;
    }
     

    public override void CharacterAttack()
    {
        if (bulletInClip > 0)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, aimTarget.position - Camera.main.transform.position, out hit))
            {
                if (hit.collider.tag == "Enemy" && hit.distance <= weaponAttackDistance)
                {
                    ShowAttackEffect(hit.point);
                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                    enemyHealth.TakeDamage(damage);
                }

                CreateBulletVisual(hit.point);
            }

            bulletInClip--;
        }
        else
        {
            audio.Play();
            isReloading = true;
        }
    }

    private void CreateBulletVisual(Vector3 hitPoint)
    {
        GameObject bulletObj = (GameObject)GameObject.Instantiate(bullet, transform.position + transform.TransformDirection(Vector3.forward), character.transform.rotation);
        bulletObj.transform.LookAt(hitPoint);
        Rigidbody bulletRB = bulletObj.GetComponent<Rigidbody>();
        bulletRB.AddForce(bulletObj.transform.TransformDirection(Vector3.forward) * 2000);
    }

    public override void EnemyAttack()
    {
        RaycastHit hit;

        if (bulletInClip > 0)
        {
            if (Physics.Raycast(character.transform.position, character.transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.collider.tag == "Player" && hit.distance <= weaponAttackDistance)
                {
                    ShowAttackEffect(hit.point);
                    CharacterHealth charHealth = hit.collider.GetComponent<CharacterHealth>();
                    charHealth.DamagePlayer(damage);
                }

                CreateBulletVisual(hit.point);
            }

            bulletInClip--;
        }
        else
        {
            isReloading = true;
        }
    }

    public override void WeaponUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReloading && bulletInClip < maxClipAmount)
            {
                isReloading = true;
            }
        }

        if (isReloading)
        {
            timer += Time.deltaTime;

            if (timer >= reloadTime)
            {
                Reload();
                isReloading = false;
                timer = 0.0f;
                audio.Stop();
            }
        }
    }

    void Reload()
    {
        if (totalAmmo > 0)
        {
            int requiredBulletsForClip = maxClipAmount - bulletInClip;

            if (totalAmmo >= requiredBulletsForClip)
            {
                bulletInClip += requiredBulletsForClip;
                totalAmmo -= requiredBulletsForClip;
            }
            else
            {
                bulletInClip += totalAmmo;
                totalAmmo = 0;
            }
        }
    }

    void ShowAttackEffect(Vector3 spawnPosition)
    {

    }
}
