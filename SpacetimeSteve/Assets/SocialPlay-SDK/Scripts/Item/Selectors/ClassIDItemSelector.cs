using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClassIDItemSelector : ItemDataSelector
{
    public override bool isItemSelected(ItemData item, IEnumerable classIDs)
    {
        foreach (int classID in classIDs)
        {
            if (classID == item.itemType)
            {
                return true;
            }
        }
        return false;
    }
}
