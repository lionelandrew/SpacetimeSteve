using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class BehaviourItemSelector : ItemDataSelector
{
    public override bool isItemSelected(ItemData item, IEnumerable behaviourPairs)
    {
        foreach (string behaviourString in behaviourPairs)
        {
            ItemFilterSystem.BehaviourPair pair = JsonConvert.DeserializeObject<ItemFilterSystem.BehaviourPair>(behaviourString);
            if (item.itemType == pair.classID)
            {
                foreach (BehaviourDefinition itemBehaviour in item.behaviours)
                {
                    if (itemBehaviour.ID == pair.behaviourID)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
