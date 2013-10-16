using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class MoveItemContainerOverflowAction : IItemContainerAction
{
    public List<ItemContainer> orderedContainerList = new List<ItemContainer>();

    public override bool PerformAction(ItemData data)
    {
        if (data == null)
        {
            throw new Exception("Can not execute action with null Object");
        }

        foreach (ItemContainer itemContainer in orderedContainerList)
        {
            if (itemContainer.GetContainerAddState(data).actionState == ContainerAddState.ActionState.Add || itemContainer.GetContainerAddState(data).actionState == ContainerAddState.ActionState.Swap)
            {
                ItemContainerManager.MoveItem(data, itemContainer);
                return true;
            }
                
        }
        return true;
    }
}


