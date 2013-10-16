using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public int Speed = 5;

    Transform target;
    CharacterController controller;
    Weapon weapon;

    float hopTime = 0.0f;
    float hopMaxTimer = 0.3f;
    bool isUp = false;

    public void InitEnemyMovement(Weapon newWeapon)
    {
        weapon = newWeapon;
        target = GameManager.player.transform;
        controller = GetComponent<CharacterController>();
    }

	// Update is called once per frame
	void Update () {
        if (target != null && weapon != null)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

            Vector3 movedirection = transform.TransformDirection(Vector3.forward);

            movedirection = movedirection.normalized;

            movedirection *= Time.deltaTime;

            if (Vector3.Distance(target.position, transform.position) > weapon.weaponAttackDistance)
            {
                EnemyHopOnMove();
                controller.Move(movedirection * Speed);
            }
        }
	}

    void EnemyHopOnMove()
    {
        hopTime += Time.deltaTime;

        if (hopTime >= hopMaxTimer)
        {
            if (isUp)
            {
                transform.position += Vector3.up * 0.1f;
                isUp = false;
            }
            else
            {
                transform.position += Vector3.down * 0.1f;
                isUp = true;
            }
            hopTime = 0.0f;
        }
    }
}
