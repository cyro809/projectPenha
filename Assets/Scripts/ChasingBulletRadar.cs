using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBulletRadar : MonoBehaviour
{
    // Start is called before the first frame update
    public ChasingBullet chasingBullet;
    Rigidbody rb;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void beFired(float speed) {
        rb = GetComponent<Rigidbody>();
		rb.AddForce (transform.forward * speed);
	}

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.CompareTag("Enemy")) {
            if(!chasingBullet.IsLockedOn()) {
                chasingBullet.target = col.gameObject.transform;
                chasingBullet.SetLockOn();
                SphereCollider collider = gameObject.GetComponent<SphereCollider>();
                collider.enabled = false;
            }

        }
    }
}
