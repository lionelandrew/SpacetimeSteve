using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System;

public class EnviromentContainerLoader : MonoBehaviour
{

    void Start()
    {
        ItemConversion.converter = new GameObjectItemDataConverter();
    }

    public void LoadAllContainerItems()
    {
        foreach (LoadItemsForContainer loader in GameObject.FindObjectsOfType(typeof(LoadItemsForContainer)))
        {
            loader.LoadItems();
        }
    }
}
