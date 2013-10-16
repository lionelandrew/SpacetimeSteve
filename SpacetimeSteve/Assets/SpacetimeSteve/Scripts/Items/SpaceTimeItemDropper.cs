using UnityEngine;
using System.Collections;

public class SpaceTimeItemDropper : MonoBehaviour {

    public GameObject itemDrop;
    public GameObject ammoDrop;

    public void DropItem()
    {
        int randomNumber = Random.Range(0, 101);

        if(randomNumber > 90)
            GameObject.Instantiate(itemDrop, transform.position, transform.rotation);
        else if(randomNumber > 60 && randomNumber < 69)
            GameObject.Instantiate(ammoDrop, transform.position, transform.rotation);
    }
}
