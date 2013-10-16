using UnityEngine;
using System.Collections;
using SocialPlay.ItemSystems;

public class NGUIContainerGameItem : MonoBehaviour {

    public ItemData itemData;
    public UILabel itemAmountLabel;
    public GameObject NGUITexture;

    void OnEnable()
    {
        UIEventListener.Get(gameObject).onClick += ItemClicked;
        UIEventListener.Get(gameObject).onDoubleClick += ItemDoubleClicked;
        //UIEventListener.Get(gameObject).onPress += ItemPressed;
        //UIEventListener.Get(gameObject).onDrag += ItemDragged;
    }

    void OnDisable()
    {
        UIEventListener.Get(gameObject).onClick -= ItemClicked;
        UIEventListener.Get(gameObject).onDoubleClick -= ItemDoubleClicked;
        //UIEventListener.Get(gameObject).onPress -= ItemPressed;
        //UIEventListener.Get(gameObject).onDrag -= ItemDragged;
    }

    void Start()
    {
        UISprite slicedSprite = gameObject.transform.FindChild("Image").GetComponent<UISprite>();
        if (slicedSprite)
        {
            if (slicedSprite.atlas.GetSprite(itemData.baseItemID.ToString()) != null)
            {
                slicedSprite.spriteName = itemData.baseItemID.ToString();
            }
            else
            {
                Destroy(slicedSprite.gameObject);
                SetImageTexture();
            }
        }
    }

    private void SetImageTexture()
    {
        GameObject newNGUITexture = GameObject.Instantiate(NGUITexture) as GameObject;
        newNGUITexture.transform.parent = transform;
        newNGUITexture.transform.localPosition = Vector3.zero;
        newNGUITexture.transform.localScale = new Vector3(50, 50, 1);

        Debug.Log(itemData.imageName);
        GetItemTexture(itemData.imageName);
    }

    public void GetItemTexture(string URL)
    {
        WWW www = new WWW(URL);

        StartCoroutine(OnReceivedItemTexture(www));
    }

    IEnumerator OnReceivedItemTexture(WWW www)
    {
        yield return www;

        UITexture uiTexture = gameObject.GetComponentInChildren<UITexture>();
        uiTexture.material = new Material(uiTexture.material.shader);
        uiTexture.mainTexture = www.texture;
        uiTexture.transform.localPosition -= Vector3.forward * 2;
    }

    public void ItemClicked(GameObject go)
    {
        itemData.ownerContainer.OnItemSingleClick(itemData);
    }

    public void ItemDoubleClicked(GameObject go)
    {
        itemData.ownerContainer.OnItemDoubleCliked(itemData);
    }

    void Update()
    {
        if (itemData == null)
            itemData = GetComponent<ItemData>();

        itemAmountLabel.text = itemData.stackSize.ToString();
    }
}
