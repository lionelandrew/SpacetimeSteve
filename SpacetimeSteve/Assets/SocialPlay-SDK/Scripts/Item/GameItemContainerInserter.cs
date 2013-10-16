using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using SocialPlay.Data;

public class GameItemContainerInserter : MonoBehaviour, IItemPutter
{
    public ItemContainer container;

    public void PutGameItem(List<ItemData> items)
    {
        if (container == null)
            throw new Exception("You must provide a container to your GameItemContainerInserter");
        else
        {
            foreach (ItemData item in items)
            {
                ItemContainerManager.MoveItem(item, container);
                Destroy(item.gameObject);
            }
        }
    }

}
