using UnityEngine;
using System.Collections;

public class WeaponGUI : MonoBehaviour {

    public Transform currentWeaponPointer;

    public Transform slotOne;
    public Transform slotTwo;
    public Transform slotThree;

    public UILabel currentWeaponAmmoLabel;

    public UISlider reloadBar;

    void Update()
    {
        if (GameManager.player != null)
        {
            CharacterWeaponManager weaponManager = GameManager.player.GetComponent<CharacterWeaponManager>();

            if (weaponManager.currentlyEquippedWeapon != null)
            {
                if (weaponManager.currentlyEquippedWeapon.ammoType == Weapon.AmmoType.bullet)
                {
                    BulletWeapon bulletWeapon = weaponManager.currentlyEquippedWeapon as BulletWeapon;
                    if (!bulletWeapon.isReloading)
                    {
                        reloadBar.gameObject.SetActive(false);
                        currentWeaponAmmoLabel.text = bulletWeapon.bulletInClip + "/" + bulletWeapon.maxClipAmount + "  " + bulletWeapon.totalAmmo;
                    }
                    else
                    {
                        reloadBar.gameObject.SetActive(true);
                        reloadBar.sliderValue = bulletWeapon.timer / bulletWeapon.reloadTime;
                        currentWeaponAmmoLabel.text = "Reloading...";
                    }
                }
                else if (weaponManager.currentlyEquippedWeapon.ammoType == Weapon.AmmoType.projectile)
                {
                    currentWeaponAmmoLabel.text = CharacterWeaponManager.ProjectileAmmo.ToString();
                }
                else
                {
                    currentWeaponAmmoLabel.text = "N/A";
                }
            }
            else
                currentWeaponAmmoLabel.text = "N/A";


            if (weaponManager.currentWeaponSlot == 0)
            {
                currentWeaponPointer.parent = slotOne;
            }
            else if (weaponManager.currentWeaponSlot == 1)
            {
                currentWeaponPointer.parent = slotTwo;
            }
            else
            {
                currentWeaponPointer.parent = slotThree;
            }

            currentWeaponPointer.localPosition = Vector3.zero;
        }
    }
}
