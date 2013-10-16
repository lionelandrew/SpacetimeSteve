using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    float attackTimer = 0.0f;
    public float attackMaxTimer = 2.0f;

    public GameObject enemyWeapon;
    Weapon weapon;

    public void EquipWeapon(GameObject newWeapon)
    {
        enemyWeapon = newWeapon;

        weapon = enemyWeapon.GetComponent<Weapon>();
        weapon.EquipWeapon(gameObject);
    }

	// Update is called once per frame
	void Update () {
        if (enemyWeapon == null)
            return;


        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.collider.tag == "Player" && hit.distance <= weapon.weaponAttackDistance)
            {
                weapon.Attack();
                attackTimer = 0.0f;
            }
        }
        
	}

    void UnequipCurrentItem()
    {
        Destroy(enemyWeapon.gameObject);
        enemyWeapon = null;
        
    }
}
