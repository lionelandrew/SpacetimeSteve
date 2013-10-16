using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyConstructor : MonoBehaviour {

    public ItemPrefabInitilizer itemPrefabInitializer;
    public GameObject enemyPrefab;
    public List<Texture2D> eratextures;
    public List<GameObject> eraWeapons;

    public GameObject ContructEnemy(int wave, int era, Vector3 spawnPoint)
    {
        GetEraTextureIndex(era);

        GameObject enemyObj = (GameObject)GameObject.Instantiate(enemyPrefab, spawnPoint, transform.rotation);
        EnemyAttack enemyAttack = enemyObj.GetComponent<EnemyAttack>();

        GameObject eraWeapon = (GameObject)GameObject.Instantiate(eraWeapons[GetEraWeaponIndex(era)]);
        ModifyWeaponForWave(eraWeapon);
        enemyAttack.EquipWeapon(eraWeapon);

        EnemyMovement enemyMovement = enemyObj.GetComponent<EnemyMovement>();
        enemyMovement.InitEnemyMovement(eraWeapon.GetComponent<Weapon>());

        SetEnemyTexture(enemyObj, era);
        SetEnemyAttributres(enemyObj, wave);
        SetEnemyDrops(enemyObj, era);

        ItemPutterDropper itemPutterDropper = enemyObj.GetComponentInChildren<ItemPutterDropper>();
        itemPutterDropper.PrefabInitilizer = itemPrefabInitializer;

        return gameObject;
    }

    void SetEnemyTexture(GameObject enemyObj, int era)
    {
        GameObject modelObject = enemyObj.transform.FindChild("default").gameObject;
        modelObject.renderer.material.mainTexture = eratextures[GetEraTextureIndex(era)];
    }

    void SetEnemyAttributres(GameObject enemyObj, int wave)
    {
        EnemyHealth enemyHealth = enemyObj.GetComponent<EnemyHealth>();
        enemyHealth.enemyHealth += (int)((float)enemyHealth.enemyHealth * (wave * 0.1f));
    }

    void SetEnemyDrops(GameObject enemyObj, int era)
    {
        ItemGetter itemGetter = enemyObj.GetComponentInChildren<ItemGetter>();

        if (era >= 0 && era <= 3)
            itemGetter.AndTags.Add("OldEra");
        if (era >= 4 && era <= 9)
            itemGetter.AndTags.Add("MidEra");
        if (era >= 10 && era <= 12)
            itemGetter.AndTags.Add("ModeraEra");
    }

    int GetEraTextureIndex(int era)
    {
        return era;
    }

    int GetEraWeaponIndex(int era)
    {
        int randomMinRange = 0;
        int randomMaxRange = 0;

        switch (era)
        {
            case 0:
                randomMinRange = 0;
                randomMaxRange = 2;
                break;
            case 1:
                randomMinRange = 0;
                randomMaxRange = 2;
                break;
            case 2:
                randomMinRange = 0;
                randomMaxRange = 2;
                break;
            case 3:
                randomMinRange = 0;
                randomMaxRange = 2;
                break;
            case 4:
                randomMinRange = 2;
                randomMaxRange = 4;
                break;
            case 5:
                randomMinRange = 2;
                randomMaxRange = 4;
                break;
            case 6:
                randomMinRange = 2;
                randomMaxRange = 4;
                break;
            case 7:
                randomMinRange = 2;
                randomMaxRange = 4;
                break;
            case 8:
                randomMinRange = 3;
                randomMaxRange = 5;
                break;
            case 9:
                randomMinRange = 3;
                randomMaxRange = 5;
                break;
            default:
                randomMinRange = 0;
                randomMaxRange = 2;
                break;
        }

        int weaponIndex = Random.Range(randomMinRange, randomMaxRange);
        return weaponIndex;
    }

    void ModifyWeaponForWave(GameObject weaponObj)
    {
        Weapon weapon = weaponObj.GetComponent<Weapon>();
        weapon.damage = (int)(weapon.damage * ((WaveManager.waveNumber +1) * 0.5f));

        if (weapon.damage <= 0)
            weapon.damage = 1;
    }
}
