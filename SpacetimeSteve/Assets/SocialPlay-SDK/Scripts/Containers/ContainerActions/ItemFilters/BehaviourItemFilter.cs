using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BehaviourItemFilter : ContianerItemDataFilter
{

    public List<BehaviourPair> behaviours = new List<BehaviourPair>();

    override public bool CheckFilter(ItemData item)
    {
        bool found = false;
        foreach (BehaviourPair behaviour in behaviours)
        {
            if (item.itemType == behaviour.classID)
            {
                foreach (BehaviourDefinition itemBehaviour in item.behaviours)
                {
                    if (itemBehaviour.ID == behaviour.behaviourID)
                    {
                        found = true;
                    }
                }
            }
        }
        if (type == InvertedState.excluded)
        {
            found = !found;
        }
        return found;
    }

    [System.Serializable]
    public class BehaviourPair
    {
        public int classID;
        public int behaviourID;
    }
}
