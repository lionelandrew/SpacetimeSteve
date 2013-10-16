using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class WebItemServiceInjector : MonoBehaviour {

    //string URL = "http://192.168.0.197/webservice/cloudgoods/cloudgoodsservice.svc/";
    string URL = "https://SocialPlayWebService.azurewebsites.net/cloudgoods/cloudgoodsservice.svc/";

    void Awake()
    {
        ItemServiceManager.service = new WebItemService(URL);
        SocialPlay.ServiceClient.Open.Url = URL;
    }
}
