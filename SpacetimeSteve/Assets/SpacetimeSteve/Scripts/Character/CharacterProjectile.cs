using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public int damageAmount = 0;
    public bool isPlayer = true;

    void OnCollisionEnter(Collision collision)
    {
        if (isPlayer)
        {
            if (collision.collider.tag == "Player")
                return;

            if (collision.collider.tag == "Enemy")
            {
                EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damageAmount);
            }
        }
        else
        {
            if (collision.collider.tag == "Enemy")
                return;

            if (collision.collider.tag == "Player")
            {
                CharacterHealth characterHealth = collision.collider.GetComponent<CharacterHealth>();
                characterHealth.DamagePlayer(damageAmount);
            }
        }


        Destroy(gameObject);
    }
}
