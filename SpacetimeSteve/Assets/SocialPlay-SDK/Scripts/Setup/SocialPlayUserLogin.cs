using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocialPlay.ItemSystems;
using CloudGoodsSDK.Models;
using CloudGoods;


public class SocialPlayUserLogin : MonoBehaviour {

    public static Action<string> OnUserAuthEvent;
    EnviromentContainerLoader containerLoader;
    public string AppID;

    WebPlatformLink webPlatformLink;

    // Use this for initialization
    void Start()
    {
        Systems.AppId = new Guid(AppID);
        containerLoader = GetComponent<EnviromentContainerLoader>();

#if UNITY_EDITOR
        Debug.Log("In Editor, Logging in as Test User with access token: " + AppID);
        var socialPlayUserObj = new PlatformUser();
        socialPlayUserObj.appID = new Guid(AppID);
        socialPlayUserObj.platformID = 1;
        socialPlayUserObj.platformUserID = "1";
        socialPlayUserObj.userName = "Test User";

        Systems.UserGetter.GetSocialPlayUser(socialPlayUserObj, OnUserAuthorized);

        return;
#endif
        WebPlatformLink.OnRecievedUser += OnUserAuthorized;
        webPlatformLink = new WebPlatformLink();
        webPlatformLink.Initiate();
     
    }


    public void OnUserAuthorized(WebserviceCalls.UserGuid socialplayMsg)
    {

        new ItemSystemGameData(AppID, socialplayMsg.userGuid, 1, Guid.NewGuid().ToString());

        Debug.Log("Logged in as user " + socialplayMsg.userName + " : " + socialplayMsg.userGuid);

        GetGameSession(ItemSystemGameData.UserID, 1, OnRegisteredSession);

    }

    public void GetGameSession(Guid UserID, int instanceID, Action<Guid> callback)
    {
        WebserviceCalls webserviceCalls = GameObject.FindObjectOfType(typeof(WebserviceCalls)) as WebserviceCalls;
        //todo remove email
        webserviceCalls.RegisterGameSession(UserID, 1, (x) =>
        {
            Debug.Log(x);
            JToken token = JToken.Parse(x);
            Guid sessionGuid = new Guid(token.ToString());

            callback(sessionGuid);
        });
    }

    void OnRegisteredSession(Guid sessionID)
    {
        ItemSystemGameData.SessionID = sessionID;

        containerLoader.LoadAllContainerItems();

        if (OnUserAuthEvent != null)
            OnUserAuthEvent(ItemSystemGameData.UserID.ToString());
    }
}
