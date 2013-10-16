using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[System.Serializable]
public class UseItemContainerAction :IItemContainerAction
{
    public override bool PerformAction(ItemData data)
    {
        if (data == null)
            throw new Exception("Can not perform action on null items");
        data.UseItem();
        return true;
    }
}

