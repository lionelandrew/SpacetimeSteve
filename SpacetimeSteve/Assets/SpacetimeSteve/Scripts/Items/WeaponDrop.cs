using UnityEngine;
using System.Collections;

public class WeaponDrop : MonoBehaviour {

    ItemData itemData;

    // Use this for initialization
    void Start()
    {
        if (GetComponent<BoxCollider>() == null)
        {
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
        }

        Destroy(gameObject, 4.0f);

        itemData = GetComponent<ItemData>();

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if(ItemContainerManager.MoveItem(itemData, GameManager.equipmentContainer) != ContainerAddState.ActionState.No)
                Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0.1f, 0));

        if (GameManager.gameState == GameManager.GameState.finished)
        {
            Destroy(gameObject);
            Debug.Log("Destroy");
        }
    }
}
