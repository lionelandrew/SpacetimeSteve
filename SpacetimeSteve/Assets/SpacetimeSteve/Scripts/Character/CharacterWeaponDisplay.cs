using UnityEngine;
using System.Collections;

public class CharacterWeaponDisplay : MonoBehaviour {

    public AudioClip switchWeaponAudio;

    public UISlider weaponOneSlider;
    public UITexture weaponOneTexture;

    public UISlider weaponTwoSlider;
    public UITexture weaponTwoTexture;

    public UISlider weaponThreeSlider;
    public UITexture weaponThreeTexture;

    public CharacterWeaponManager characterWeaponManager;

    public UILabel ammoLabel;
    public UISlider slider;

    bool isInitialized = false;

    void Awake()
    {
        Debug.Log("display start");
        CharacterWeaponManager.AddedWeapon += AddWeaponDisplay;
        CharacterWeaponManager.SwitchedWeapons += SwitchWeaponDisplay;
        CharacterWeaponManager.DroppedWeapon += DroppedWeapon;
    }
	
	// Update is called once per frame
	void Update () {

        if (GameManager.gameState == GameManager.GameState.finished)
        {
            isInitialized = false;

            return;
        }

        if (characterWeaponManager == null || characterWeaponManager.currentlyEquippedWeapon == null)
            return;

        Weapon weapon = characterWeaponManager.currentlyEquippedWeapon;
        if (weapon.hasAttacked)
        {
            float coolDownValue = weapon.attackRate - weapon.cooldownTimer;

            slider.sliderValue = 1 - ((weapon.cooldownTimer / weapon.attackRate));
        }
        else
            slider.sliderValue = 0;

        DisplayWeaponAmmo(weapon);
	}

    void DisplayWeaponAmmo(Weapon weapon)
    {
        if (weapon.ammoType == Weapon.AmmoType.bullet)
        {
            BulletWeapon bulletWeapon = characterWeaponManager.currentlyEquippedWeapon as BulletWeapon;
            ammoLabel.text = bulletWeapon.bulletInClip + "/" + bulletWeapon.maxClipAmount + "  " +  bulletWeapon.totalAmmo;
        }
        else if (weapon.ammoType == Weapon.AmmoType.projectile)
        {
            ammoLabel.text = CharacterWeaponManager.ProjectileAmmo.ToString();
        }
        else
        {
            ammoLabel.text = "N/A";
        }
    }

    void AddWeaponDisplay(int weaponIn)
    {
        if(characterWeaponManager == null)
            characterWeaponManager = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterWeaponManager>();

        if (weaponIn == 0)
            weaponOneTexture.mainTexture = characterWeaponManager.weaponOne.weaponTexture;
        else if(weaponIn == 1)
            weaponTwoTexture.mainTexture = characterWeaponManager.weaponTwo.weaponTexture;
        else if (weaponIn == 2)
            weaponThreeTexture.mainTexture = characterWeaponManager.weaponThree.weaponTexture;
    }

    void SwitchWeaponDisplay(int weaponOut, int weaponIn)
    {
        audio.PlayOneShot(switchWeaponAudio);

        if (weaponIn == 0)
        {
            weaponOneSlider.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            weaponOneSlider.transform.localPosition = new Vector3(weaponOneSlider.transform.localPosition.x, -20, weaponOneSlider.transform.localPosition.z);
            slider.sliderValue = 0;
            slider = weaponOneSlider;

            if(weaponOut == 1)
                weaponTwoSlider.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            else if(weaponOut == 2)
                weaponThreeSlider.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
        else if (weaponIn == 1)
        {
            weaponTwoSlider.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            weaponTwoSlider.transform.localPosition = new Vector3(weaponTwoSlider.transform.localPosition.x, -20, weaponTwoSlider.transform.localPosition.z);
            slider.sliderValue = 0;
            slider = weaponTwoSlider;

            if (weaponOut == 0)
                weaponOneSlider.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            else if (weaponOut == 2)
                weaponThreeSlider.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
        else
        {
            weaponThreeSlider.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            weaponThreeSlider.transform.localPosition = new Vector3(weaponThreeSlider.transform.localPosition.x, -20, weaponThreeSlider.transform.localPosition.z);
            slider.sliderValue = 0;
            slider = weaponThreeSlider;

            if (weaponOut == 0)
                weaponOneSlider.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            else if (weaponOut == 1)
                weaponTwoSlider.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
    }

    public void DroppedWeapon(int weaponSlot)
    {
        Debug.Log("dropped weapon event");
    }

}
