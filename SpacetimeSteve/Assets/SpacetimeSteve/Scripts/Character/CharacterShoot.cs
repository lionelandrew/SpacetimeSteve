using UnityEngine;
using System.Collections;

public class CharacterShoot : MonoBehaviour {

    public Transform aimTarget;
    public GameObject bulletPrefab;
	
	// Update is called once per frame
	void Update () {
        Screen.lockCursor = true;


        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = (GameObject)GameObject.Instantiate(bulletPrefab, transform.position + Vector3.forward, Quaternion.LookRotation(aimTarget.position - transform.position));
            Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();

            bulletRigidBody.AddForce(bulletRigidBody.transform.forward * 2000);
        }
	}
}
