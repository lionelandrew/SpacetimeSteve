using UnityEngine;
using System.Collections;

public class AmmoDrop : MonoBehaviour {

    ItemData itemData;

	// Use this for initialization
	void Start () {
        BoxCollider boxCollider;

        if (GetComponent<BoxCollider>() == null)
            boxCollider = gameObject.AddComponent<BoxCollider>();
        else
            boxCollider = GetComponent<BoxCollider>();

        boxCollider.isTrigger = true;

        Destroy(gameObject, 4.0f);
	}

    void OnTriggerEnter(Collider collider)
    {
        itemData = GetComponent<ItemData>();

        if (collider.tag == "Player")
        {
            switch (itemData.itemName)
            {
                case "Projectile Ammo":
                    CharacterWeaponManager.ProjectileAmmo += int.Parse(itemData.stats["Amount"].ToString());
                    break;
                case "Bullet Ammo":
                    CharacterWeaponManager.BulletAmmo += int.Parse(itemData.stats["Amount"].ToString());
                    break;
                default:
                    break;

            }

            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0.1f, 0));

        if (GameManager.gameState == GameManager.GameState.finished)
            Destroy(gameObject);
    }
}
