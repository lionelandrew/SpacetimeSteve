using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TagItemSelector : ItemDataSelector
{
    public override bool isItemSelected(ItemData item, IEnumerable tagList)
    {
        foreach (string tag in tagList)
        {
            if (item.tags.Contains(tag))
            {
                return true;
            }
        }
        return false;
    }
}
