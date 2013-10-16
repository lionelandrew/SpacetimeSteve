using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemFilterSystem
{

    public enum SetupByType
    {
        classID, tag, behaviour
    }

    public SetupByType selectorType = SetupByType.tag;

    [HideInInspector]
    public List<int> classList = new List<int>();
    [HideInInspector]
    public List<string> TagList = new List<string>();
    [HideInInspector]
    public List<string> behaviours = new List<string>();

    public bool IsSelected(ItemData item)
    {
       
        switch (selectorType)
        {
            case SetupByType.classID:
                return new ClassIDItemSelector().isItemSelected(item, classList);
            case SetupByType.tag:
                return new TagItemSelector().isItemSelected(item, TagList);
            case SetupByType.behaviour:
                return new BehaviourItemSelector().isItemSelected(item, behaviours);
            default:
                return false;
        }
    }

    public class BehaviourPair
    {
        public int behaviourID;
        public int classID;

        public BehaviourPair()
        {
            behaviourID = 0;
            classID = 0;
        }
    }

}
