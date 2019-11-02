using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float speed;
	public GameObject enemy;
	private float pushBackForce = 1000;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * speed * Time.deltaTime);	
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "Enemy" || col.gameObject.name == "Enemy (1)") {
			Enemy hitEnemy = col.gameObject.GetComponent<Enemy>();
			hitEnemy.getHit (pushBackForce, transform.position);
			Destroy (gameObject);
		}
	}
}
