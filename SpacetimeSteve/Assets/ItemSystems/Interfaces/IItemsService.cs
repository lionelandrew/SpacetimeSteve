using System;
using System.Collections.Generic;
using SocialPlay.Generic;
using UnityEngine;
using SocialPlay.Data;


public interface IItemService
{

    void GenerateItemsAtLocation(string OwnerID, string OwnerType, int Location, Guid AppID, int MinimumEnergyOfItem, int TotalEnergyToGenerate, Action<string> callback, string ANDTags = "", string ORTags = "");


    void GetOwnerItems(string ownerID, string ownerType, int location, Guid AppID, Action<string> callback);


    void MoveItemStack(Guid StackToMove, int MoveAmount, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<string> callback);

    void MoveItemStacks(string stacks, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<string> callback);

    void RemoveItemStack(Guid StackRemove, Action<string> callback);


    void RemoveItemStacks(List<Guid> StacksToRemove, Action<string> callback);
}
