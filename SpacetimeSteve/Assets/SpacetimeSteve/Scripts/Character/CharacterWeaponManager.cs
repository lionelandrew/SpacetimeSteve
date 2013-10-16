using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterWeaponManager : MonoBehaviour {

    public static Action<int, int> SwitchedWeapons;
    public static Action<int> AddedWeapon;
    public static Action<int> DroppedWeapon;

    public static int ProjectileAmmo = 50;
    public static int BulletAmmo = 200;

    public Weapon weaponOne;
    public Weapon weaponTwo;
    public Weapon weaponThree;

    public Weapon currentlyEquippedWeapon;
                 
    public int currentWeaponSlot = 0;

    public GameObject blockParticle;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }

        if (Input.GetMouseButton(0))
        {
            if(currentlyEquippedWeapon != null)
                currentlyEquippedWeapon.Attack();
        }

        if (currentWeaponSlot == 0)
        {
            CheckForBlock();
        }
       
    }

    private void CheckForBlock()
    {
        CharacterHealth characterHealth = GetComponent<CharacterHealth>();

        if (Input.GetMouseButton(1))
        {
            characterHealth.isBlocking = true;
            blockParticle.SetActive(true);
        }
        else
        {
            characterHealth.isBlocking = false;
            blockParticle.SetActive(false);
        }
    }

    public void ClearWeapons()
    {
        if (currentlyEquippedWeapon != null)
        {
            currentlyEquippedWeapon.Unequip();
            currentlyEquippedWeapon = null;
        }

        weaponOne = null;
        weaponTwo = null;
        weaponThree = null;
    }

    public bool AddWeapon(Weapon newWeapon, int slot)
    {
        if (currentlyEquippedWeapon == null)
        {
            currentlyEquippedWeapon = newWeapon;
            newWeapon.EquipWeapon(this.gameObject);
            currentWeaponSlot = slot - 1;
        }

        if (slot == 1 && weaponOne == null)
        {
            return SetAddWeapon(ref weaponOne, newWeapon, 0);
        }
        else if (slot == 2 && weaponTwo == null)
        {
            return SetAddWeapon(ref weaponTwo, newWeapon, 1);
        }
        else if (slot == 3 && weaponThree == null)
        {
            return SetAddWeapon(ref weaponThree, newWeapon, 2);
        }
        else
        {
            return false;
        }
    }

    public void RemoveWeapon(Weapon removeWeapon, int slot)
    {
        if (slot == 1)
            weaponOne = null;
        else if (slot == 2)
            weaponTwo = null;
        else if (slot == 3)
            weaponThree = null;

        if (removeWeapon == currentlyEquippedWeapon)
        {
            currentlyEquippedWeapon.Unequip();
            currentlyEquippedWeapon = null;

            if (weaponOne != null)
            {
                currentlyEquippedWeapon = weaponOne;
                currentWeaponSlot = 0;
            }
            else if (weaponTwo != null)
            {
                currentlyEquippedWeapon = weaponTwo;
                currentWeaponSlot = 1;
            }
            else if (weaponThree != null)
            {
                currentlyEquippedWeapon = weaponThree;
                currentWeaponSlot = 2;
            }

            if (currentlyEquippedWeapon != null)
                currentlyEquippedWeapon.EquipWeapon(this.gameObject);
        }
    }

    bool SetAddWeapon(ref Weapon addWeapon, Weapon newWeapon, int weaponSlot)
    {
        addWeapon = newWeapon;

        if (AddedWeapon != null)
        {
            Debug.Log("Added event call");
            AddedWeapon(weaponSlot);
        }

        return true;
    }

    public void SwitchWeapon()
    {
        if (currentWeaponSlot == 0)
        {
            if (weaponTwo == null && weaponThree == null)
                return;

            weaponOne.Unequip();

            if (weaponTwo != null)
                SetWeaponSwitch(weaponTwo, 1, 0);
            else
                SetWeaponSwitch(weaponThree, 2, 0);
        }
        else if (currentWeaponSlot == 1)
        {
            if (weaponOne == null && weaponThree == null)
                return;

            weaponTwo.Unequip();

            if (weaponThree != null)
                SetWeaponSwitch(weaponThree, 2, 1);
            else
                SetWeaponSwitch(weaponOne, 0, 1);
        }
        else if(currentWeaponSlot == 2)
        {
            if (weaponOne == null && weaponTwo == null)
                return;

            weaponThree.Unequip();

            if (weaponOne != null)
                SetWeaponSwitch(weaponOne, 0, 2);
            else
                SetWeaponSwitch(weaponTwo, 1, 2);
        }
    }

    void SetWeaponSwitch(Weapon weaponIn, int slotIn, int slotOut)
    {
        currentlyEquippedWeapon = weaponIn.EquipWeapon(this.gameObject);
        currentWeaponSlot = slotIn;

        if(SwitchedWeapons != null)
            SwitchedWeapons(slotOut, slotIn);
    }

    public void AttackWithCurrentWeapon()
    {
        currentlyEquippedWeapon.Attack();
    }
}
