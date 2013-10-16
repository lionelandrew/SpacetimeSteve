using UnityEngine;
using System.Collections;

public class CoinDrop : MonoBehaviour {

	// Use this for initialization
	void Start () {

        BoxCollider boxCollider;

        if (GetComponent<BoxCollider>() == null)
            boxCollider = gameObject.AddComponent<BoxCollider>();
        else
            boxCollider = GetComponent<BoxCollider>();

        boxCollider.isTrigger = true;

	}


    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            PlayerScore.AddPlayerScore(20);

            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.gameState == GameManager.GameState.finished)
            Destroy(gameObject);

        Destroy(gameObject, 4.0f);
	}
}
