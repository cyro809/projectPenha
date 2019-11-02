using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float speed;
	public GameObject enemy;
	private float pushBackForce = 1000;
	Rigidbody rb;

	void OnBecameInvisible() {
		Destroy (gameObject);
	}

	public void beFired(float speed) {
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (transform.forward * speed);
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "Enemy" || col.gameObject.name == "Enemy (1)") {
			Enemy hitEnemy = col.gameObject.GetComponent<Enemy>();
			hitEnemy.getHit (pushBackForce, transform.position);
			Destroy (gameObject);
		}
	}
}
