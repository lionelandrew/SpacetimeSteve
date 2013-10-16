using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClassIDItemFilter : ContianerItemDataFilter 
{
    public List<int> classIDs;

    override public bool CheckFilter(ItemData item)
    {
        bool found = false;
        foreach (int classID in classIDs)
        {
            if (item.itemType == classID)
            {
                found= true;
                break;
            }
        }
        if (type== InvertedState.excluded)
        {
            found = !found;
        }
        return found;
    }
}
