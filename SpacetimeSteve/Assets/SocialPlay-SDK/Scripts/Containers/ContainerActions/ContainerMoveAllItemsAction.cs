using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;
using SocialPlay.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ContainerMoveAllItemsAction : ContainerActions
{
    public ItemContainer sourceContainer;
    public ItemContainer DestinationContainer;
    public int destinationLocation;

    public override void DoAction(ItemData itemData)
    {
        ///Tmp moves all to vault on backend
        MoveMultipleStacks stacks = new MoveMultipleStacks();
        stacks.StackInfos = new List<MoveItemStackInfo>();
        foreach (ItemData item in sourceContainer.containerItems)
        {
            MoveItemStackInfo info = new MoveItemStackInfo();
            info.MoveAmount = item.stackSize;
            info.StackID = item.stackID;
            stacks.StackInfos.Add(info);
        }
        string convert = JsonConvert.SerializeObject(stacks);
        ItemServiceManager.service.MoveItemStacks(convert, ItemSystemGameData.UserID.ToString(), "User", ItemSystemGameData.AppID, destinationLocation, delegate(string x)
        {
            JToken token = JToken.Parse(x);
            MoveMultipleItemsResponse infos = JsonConvert.DeserializeObject<MoveMultipleItemsResponse>(token.ToString());
            ItemData[] containerItems = new ItemData[sourceContainer.containerItems.Count];
            sourceContainer.containerItems.CopyTo(containerItems);

            foreach (MovedItemsInfo info in infos.movedItems)
            {
                foreach (ItemData data in containerItems)
                {
                    if (data.stackID == info.OriginalStackID)
                    {
                        data.stackID = info.NewStackID;
                        sourceContainer.Remove(data, true);
                        DestinationContainer.Add(data, -1, false);
                    }
                }
            }
        });

    }


}
