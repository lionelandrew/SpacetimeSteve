using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentContainerDrop : MonoBehaviour {

    public GameObject defaultDropItemPrefab;

    public GameObject weaponSlotOne;
    public GameObject weaponSlotTwo;
    public GameObject weaponSlotThree;

    public Vector3 dropPosition;

    ItemDrop gameItemDrop;

    void Start()
    {
        gameItemDrop = gameObject.AddComponent<ItemDrop>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DropItem(weaponSlotOne.GetComponentInChildren<ItemData>());
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            DropItem(weaponSlotTwo.GetComponentInChildren<ItemData>());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DropItem(weaponSlotThree.GetComponentInChildren<ItemData>());
        }

        if (GameManager.gameState == GameManager.GameState.finished)
            ClearEquipmentItems();
    }

    void DropItem(ItemData item)
    {
        item.ownerContainer.Remove(item, false);
        gameItemDrop.DropItemIntoWorld(item, GameObject.Find("Player").transform.position + Vector3.forward, defaultDropItemPrefab);
    }

    public void ClearEquipmentItems()
    {
        if (weaponSlotOne.GetComponentInChildren<ItemData>() != null)
            weaponSlotOne.GetComponentInChildren<ItemData>().ownerContainer.Remove(weaponSlotOne.GetComponentInChildren<ItemData>(), false);

        if (weaponSlotTwo.GetComponentInChildren<ItemData>() != null)
            weaponSlotTwo.GetComponentInChildren<ItemData>().ownerContainer.Remove(weaponSlotTwo.GetComponentInChildren<ItemData>(), false);

        if (weaponSlotThree.GetComponentInChildren<ItemData>() != null)
            weaponSlotThree.GetComponentInChildren<ItemData>().ownerContainer.Remove(weaponSlotThree.GetComponentInChildren<ItemData>(), false);
    }

}
