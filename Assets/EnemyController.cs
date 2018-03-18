using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public Transform Player;
	int MoveSpeed = 7;
	bool onGround;

	Rigidbody rB;
	// Use this for initialization
	void Start () {
		rB = GetComponent<Rigidbody> ();
		onGround = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (onGround) {
			transform.LookAt (Player);
			rB.AddForce (transform.forward * MoveSpeed);
		}
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "Bullet" || col.gameObject.name == "Bullet(Clone)") {
			float magnitude = 300;
			Vector3 force = transform.position - col.transform.position;
			force.Normalize ();
			rB.AddForce (force * magnitude);
		}

		if (col.gameObject.name == "Plane") {
			onGround = true;
		} else {
			onGround = false;
		}
	}
}
