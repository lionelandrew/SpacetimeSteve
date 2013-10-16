using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeapons : MonoBehaviour {

    public List<Weapon> playerWeapons;

    public ItemContainer equipmentContainer;
    CharacterWeaponManager weaponManager;

    void OnEnable()
    {
        equipmentContainer.AddedItem += WeaponAdded;
        equipmentContainer.removedItem += WeaponRemoved;
    }

    void OnDisable()
    {
        equipmentContainer.AddedItem -= WeaponAdded;
        equipmentContainer.removedItem -= WeaponRemoved;
    }

    void WeaponAdded(ItemData itemData, bool isSaved)
    {
        weaponManager = GameManager.player.GetComponent<CharacterWeaponManager>();

        int slotIndex = GetSlotIndex(itemData);

        foreach (Weapon weapon in playerWeapons)
        {
            if (weapon.weaponID == itemData.itemID)
                weaponManager.AddWeapon(weapon, slotIndex);
        }
    }

    void WeaponRemoved(ItemData itemData, bool isSaved)
    {
        int slotIndex = GetSlotIndex(itemData);

        foreach (Weapon weapon in playerWeapons)
        {
            if (weapon.weaponID == itemData.itemID)
                weaponManager.RemoveWeapon(weapon, slotIndex);
        }
    }

    int GetSlotIndex(ItemData itemData)
    {
        if (itemData.itemType == 1112)
            return 1;
        else if (itemData.itemType == 1113)
            return 2;
        else 
            return 3;
    }

    void Update()
    {
        if (GameManager.gameState == GameManager.GameState.finished)
        {
            ResetAmmo();
        }
    }

    void ResetAmmo()
    {
        CharacterWeaponManager.ProjectileAmmo = 50;
        CharacterWeaponManager.BulletAmmo = 200;
    }
}
