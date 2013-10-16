﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SocialPlay.ItemSystems;

public class SlottedItemContainer : ItemContainer
{
    public static ISlotSelector slotSelector;

    public Dictionary<int, SlottedContainerSlotData> slots = new Dictionary<int, SlottedContainerSlotData>();

    public Dictionary<string, float> stats = new Dictionary<string, float>();

    public bool isSwappable;

    public void AddSlot(int SlotID, ItemData slotData, int[] ValidTypes, int slotMaxCount = 1, int priority = 0)
    {
        if (slots.ContainsKey(SlotID))
            throw new Exception("Can not add same slot twice");

        SlottedContainerSlotData newSlot = new SlottedContainerSlotData();

        newSlot.slotData = slotData;
        newSlot.validTypes = ValidTypes.ToList();
        newSlot.slotMaxCountLimit = slotMaxCount;
        newSlot.priority = priority;
        newSlot.slotNameID = SlotID.ToString();
        slots.Add(SlotID, newSlot);
        if (slotData != null)
        {
            AddItemEvent(slotData, true);
        }
    }

    public void ModdifyExistingSlot(int SlotID, int[] ValidTypes)
    {
        if (!slots.ContainsKey(SlotID))
        {
            Debug.LogError("slot does not exist to be modiffied");
            return;
        }
        SlottedContainerSlotData selectedSlot = slots[SlotID];
        selectedSlot.validTypes = ValidTypes.ToList();
    }

    public int FindItmeInSlot(ItemData item)
    {
        if (item == null)
        {
            return -1;
        }
        foreach (KeyValuePair<int, SlottedContainerSlotData> pair in slots)
        {
            if (pair.Value.slotData == null)
                continue;

            if (pair.Value.slotData.stackID.Equals(item.stackID))
            {
                return pair.Key;
            }
        }

        return -1;
    }

    protected override void AddItem(ItemData modified, int amount = -1, bool isSave = true)
    {
        if (slots.Count == 0) return; // No active slots to check.

        Dictionary<int, SlottedContainerSlotData> ShortList = GetAllAvalibleSlots(modified.itemType);


        if (ShortList.Count == 0 || ShortList == null)
        {
            return; // did not find any matching slots.
        }

        if (slotSelector == null)
            throw new Exception("Slot selector must be set before adding items to slots.");

        SlottedContainerSlotData selectedSlot = slotSelector.PickBestSlot(ShortList);

        if (selectedSlot == null)
        {
            return; // slected slot did not exist.
        }

        ItemData NewSlotData = null;

        int TargetStackSize = amount;
        if (amount == -1)
            TargetStackSize = modified.stackSize;

        if (TargetStackSize > selectedSlot.slotMaxCountLimit)
        {
            TargetStackSize = selectedSlot.slotMaxCountLimit;
        }

        modified.CreatNew(out NewSlotData, TargetStackSize, this);
        selectedSlot.slotData = NewSlotData;
        ModdifyStatsByFactor(NewSlotData, 1);
        containerItems.Add(NewSlotData);

        if(modified.ownerContainer == null)
            Destroy(modified.gameObject);

        AddItemEvent(NewSlotData, isSave);
        return;
    }

    protected Dictionary<int, SlottedContainerSlotData> GetAllAvalibleSlots(int type)
    {
        Dictionary<int, SlottedContainerSlotData> possibleSlots = new Dictionary<int, SlottedContainerSlotData>();

        foreach (KeyValuePair<int, SlottedContainerSlotData> SelectedSlots in slots)
        {

            if (SelectedSlots.Value.validTypes.Contains(type))
            {
                possibleSlots.Add(SelectedSlots.Key, SelectedSlots.Value);
            }
        }
        return possibleSlots;
    }

    protected void ModdifyStatsByFactor(ItemData data, int factor)
    {

        if (data.stats == null) return;
        foreach (KeyValuePair<string, float> StatsDataPair in data.stats)
        {
            string tmp = StatsDataPair.Key;
            if (stats.ContainsKey(StatsDataPair.Key))
            {
                stats[StatsDataPair.Key] += StatsDataPair.Value * factor;
                tmp += " Modding value tagList: " + StatsDataPair.Value * factor;
            }
            else
            {
                stats.Add(StatsDataPair.Key, StatsDataPair.Value * factor);
                tmp += "Adding new Key with :" + StatsDataPair.Value * factor;
            }
            tmp += "\n final value :=" + stats[StatsDataPair.Key];
        }
    }

    protected override void RemoveItem(ItemData modified, bool isMovingToAnotherContainer, int amount = -1)
    {
        if (slots.Count == 0)
            return;

        foreach (KeyValuePair<int, SlottedContainerSlotData> selectedSlot in slots)
        {
            if (selectedSlot.Value.slotData == null)
                continue;

            if (selectedSlot.Value.slotData.Equals(modified))
            {
                if (selectedSlot.Value.slotData.stackSize <= amount || amount == -1)
                {
                    ModdifyStatsByFactor(selectedSlot.Value.slotData, -1);
                    containerItems.Remove(selectedSlot.Value.slotData);
                    RemoveItemEvent(modified,isMovingToAnotherContainer);
                    selectedSlot.Value.slotData = null;
                }
                else
                    selectedSlot.Value.slotData.stackSize -= amount;
                return;
            }
        }
    }

    public override int Contains(ItemData modified)
    {
        if (slots.Count == 0) return 0;
        foreach (KeyValuePair<int, SlottedContainerSlotData> selectedSlot in slots)
        {
            if (selectedSlot.Value.slotData == null) continue;
            if (selectedSlot.Value.slotData.Equals(modified))
            {
                return selectedSlot.Value.slotData.stackSize;
            }
        }
        return 0;
    }

    public override void Clear()
    {
        foreach (SlottedContainerSlotData item in slots.Values)
        {
            item.slotData = null;
        }

        stats.Clear();
    }

    protected override ContainerAddState MyContainerAddState(ItemData modified)
    {

        Dictionary<int, SlottedContainerSlotData> tmp = GetAllAvalibleSlots(modified.itemType);


        List<SlottedContainerSlotData> ShortList = tmp.Values.ToList<SlottedContainerSlotData>();
        ContainerAddState state = new ContainerAddState(ContainerAddState.ActionState.No);
        ShortList.Sort((a, b) => a.priority.CompareTo(b.priority));
        if (tmp.Count != 0)
        {
            bool Swaping = false;
            SlottedContainerSlotData firstStack = null;
            foreach (SlottedContainerSlotData SelectedSlots in tmp.Values)//Check for empty slots
            {
                if (SelectedSlots.slotData == null)
                {
                    state = new ContainerAddState(ContainerAddState.ActionState.Add, SelectedSlots.slotMaxCountLimit, null);
                    return state;
                }
                else
                {
                    Swaping = true;
                    if (firstStack == null)
                    {
                        firstStack = SelectedSlots;
                    }
                }
            }
            if (Swaping && firstStack != null)
            {
                if(isSwappable)
                    state = new ContainerAddState(ContainerAddState.ActionState.Swap, firstStack.slotMaxCountLimit, firstStack.slotData);
                else
                    state = new ContainerAddState(ContainerAddState.ActionState.No, firstStack.slotMaxCountLimit, firstStack.slotData);
            }
        }

        //foreach (KeyValuePair<int, SlottedContainerSlotData> sl in slots)
        //{
        //    if (sl.Value.validTypes.Contains(modified.itemType))
        //    {
        //        if (sl.Value.slotData == null)
        //        {
        //            state = new ContainerAddState(ContainerAddState.ActionState.Add, sl.Value.slotMaxCountLimit, null);
        //        }
        //        else
        //        {
        //            state = new ContainerAddState(ContainerAddState.ActionState.Swap, sl.Value.slotMaxCountLimit, sl.Value.slotData);
        //        }
        //    }
        //}

        return state;
    }
}

