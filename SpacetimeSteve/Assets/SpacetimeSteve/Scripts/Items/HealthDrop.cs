using UnityEngine;
using System.Collections;

public class HealthDrop : MonoBehaviour {

    public int healthAmount = 20;

    void Start()
    {
        Destroy(gameObject, 4.0f);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            CharacterHealth charHealth = collider.GetComponent<CharacterHealth>();
            charHealth.HealPlayer(healthAmount);

            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0,0,0.1f));

        if (GameManager.gameState == GameManager.GameState.finished)
            Destroy(gameObject);
    }
}
