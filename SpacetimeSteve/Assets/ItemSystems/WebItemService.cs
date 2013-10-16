using System;
using System.Collections.Generic;
using SocialPlay.Generic;
using UnityEngine;
using SocialPlay.Data;
using Newtonsoft.Json;


public class WebItemService : IItemService
{
    public WebItemService(string newCloudGoodsURL)
    {
        cloudGoodsURL = newCloudGoodsURL;
    }

    public static string cloudGoodsURL = "http://192.168.0.197/webservice/cloudgoods/cloudgoodsservice.svc/";

    public void GenerateItemsAtLocation(string OwnerID, string OwnerType, int Location, Guid AppID, int MinimumEnergyOfItem, int TotalEnergyToGenerate, Action<string> callback, string ANDTags = "", string ORTags = "")
    {
        string url = string.Format("{0}GenerateItemsAtLocation?OwnerID={1}&OwnerType={2}&Location={3}&AppID={4}&MinimumEnergyOfItem={5}&TotalEnergyToGenerate={6}&ANDTags={7}&ORTags={8}", cloudGoodsURL, OwnerID, OwnerType, Location, AppID, MinimumEnergyOfItem, TotalEnergyToGenerate, ANDTags, ORTags);
        WWWPacket.Creat(url, callback);
    }

    public void GetOwnerItems(string ownerID, string ownerType, int location, Guid AppID, Action<string> callback)
    {
        string url = string.Format("{0}GetOwnerItems?ownerID={1}&ownerType={2}&location={3}&AppID={4}", cloudGoodsURL, ownerID, ownerType, location, AppID.ToString());
        WWWPacket.Creat(url, callback);
    }

    public void MoveItemStacks(string stacks, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<string> callback)
    {
        string url = string.Format("{0}MoveItemStacks?stacks={1}&DestinationOwnerID={2}&DestinationOwnerType={3}&AppID={4}&DestinationLocation={5}", cloudGoodsURL, stacks, DestinationOwnerID, DestinationOwnerType, AppID.ToString(), DestinationLocation);
        WWWPacket.Creat(url, callback);
    }

    public void MoveItemStack(Guid StackToMove, int MoveAmount, string DestinationOwnerID, string DestinationOwnerType, Guid AppID, int DestinationLocation, Action<string> callback)
    {
        string url = string.Format("{0}MoveItemStack?StackToMove={1}&MoveAmount={2}&DestinationOwnerID={3}&DestinationOwnerType={4}&AppID={5}&DestinationLocation={6}", cloudGoodsURL, StackToMove, MoveAmount, DestinationOwnerID, DestinationOwnerType, AppID.ToString(), DestinationLocation);
        WWWPacket.Creat(url, callback);
    }

    public void RemoveItemStack(Guid StackRemove, Action<string> callback)
    {
        string url = string.Format("{0}RemoveStackItem?stackID={1}", cloudGoodsURL, StackRemove);
        WWWPacket.Creat(url, callback);
    }


    public void RemoveItemStacks(List<Guid> StacksToRemove, Action<string> callback)
    {
        RemoveMultipleItems infos = new RemoveMultipleItems();
        infos.stacks = StacksToRemove;
        string stacksInfo = JsonConvert.SerializeObject(infos);
        string url = string.Format("{0}RemoveStackItems?stacks={1}", cloudGoodsURL, stacksInfo);
        WWWPacket.Creat(url, callback);
    }
}
