using SocialPlay.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class ItemData : MonoBehaviour, IEquatable<ItemData>
{
    public ItemContainer ownerContainer = null;
    public int stackSize = 0;


    public string itemName = "";
    public Guid stackID = Guid.Empty;
    public int itemType = 0;
    public int totalEnergy = 0;
    public int baseEnergy = 0;
    public int salePrice = 0;
    public List<BehaviourDefinition> behaviours = new List<BehaviourDefinition>();
    public string description = "";
    public int quality = 0;
    public string imageName = "";
    public bool isOwned = false;
    public int itemID = 0;
    public int baseItemID = 0;
    public Dictionary<string, float> stats;
    public string assetURL;
    public List<string> tags;


    public void AssetBundle(Action<UnityEngine.Object> callBack)
    {
        try
        {
            SocialPlay.Bundles.BundleSystem.Get(assetURL, callBack, "Items", true);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            callBack(null);
        }
    }

    public virtual void CreatNew(out ItemData newItem, int amount, ItemContainer ownerContainer)
    {
        newItem = NewItem();
        newItem.stackSize = amount;
        newItem.ownerContainer = ownerContainer;
        newItem.itemName = itemName;
        newItem.itemType = itemType;
        newItem.stackID = stackID;
        newItem.itemType = itemType;
        newItem.totalEnergy = totalEnergy;
        newItem.baseEnergy = baseEnergy;
        newItem.salePrice = salePrice;
        newItem.behaviours = behaviours;
        newItem.description = description;
        newItem.quality = quality;
        newItem.imageName = imageName;
        newItem.isOwned = isOwned;
        newItem.itemID = itemID;
        newItem.stats = stats;
        newItem.assetURL = assetURL;
        newItem.tags = tags;
    }

    /// <summary>
    /// Used to create new version of this item
    /// </summary>
    /// <returns>new ItemData(); (Overrider for each derived class)</returns>
    protected virtual ItemData NewItem()
    {

        GameObject tmp = Instantiate(this.gameObject) as GameObject;
        tmp.name = itemName;
        return tmp.GetComponent<ItemData>();
    }

    public virtual bool UseItem()
    {
        return false;
    }


    public bool Equals(ItemData other)
    {
        if (this == null || other == null)
        {
            return false;
        }
        if (itemID == other.itemID && isOwned == other.isOwned)
            return true;
        else return false;
    }

    public bool IsStackable(ItemData other)
    {
        if (this == null || other == null)
        {
            return false;
        }
        if (itemID == other.itemID && isOwned == other.isOwned)
            return true;
        else return false;
    }

    public void UpdateStackID(string newStackID)
    {
        stackID = new Guid(JToken.Parse(newStackID).ToString());
    }

    public virtual void ExtraGameConversions(ItemDetail detail, GameObject spawnedObject)
    {

    }

    public void SetItemData(ItemData itemData)
    {
        stackSize = itemData.stackSize;
        ownerContainer = itemData.ownerContainer;
        itemName = itemData.itemName;
        itemType = itemData.itemType;
        baseItemID = itemData.baseItemID;
        stackID = itemData.stackID;
        itemType = itemData.itemType;
        totalEnergy = itemData.totalEnergy;
        baseEnergy = itemData.baseEnergy;
        salePrice = itemData.salePrice;
        behaviours = itemData.behaviours;
        description = itemData.description;
        quality = itemData.quality;
        imageName = itemData.imageName;
        isOwned = itemData.isOwned;
        itemID = itemData.itemID;
        stats = itemData.stats;
        assetURL = itemData.assetURL;
        tags = itemData.tags;
    }
}

