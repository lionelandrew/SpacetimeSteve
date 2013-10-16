using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[System.Serializable]
public class ItemContainerInteractions : MonoBehaviour
{
    protected ItemContainer sourceContainer = null;
    public List<ContainerInputAction> possibleActions = new List<ContainerInputAction>();

    void Awake()
    {
        sourceContainer = this.GetComponent<ItemContainer>();
    }

    public virtual void PerformAction(string inputType, ItemData data)
    {
        if (sourceContainer == null)
            throw new Exception("Source container of actions can not be null.");

        if (data == null)
        {
            throw new Exception("Invalid item passed to Item Container Interactions");
        }

        foreach (ContainerInputAction inputAction in possibleActions)
        {
            if (inputAction.inputType == inputType)
            {
                foreach (IItemContainerAction containerAction in inputAction.ContainerActions)
                {
                    if (containerAction.PerformAction(data))
                    {
                        return;
                    }
                }
                return;
            }
        }
    }
}

