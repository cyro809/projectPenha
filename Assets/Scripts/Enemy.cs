using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject {
	public Transform Player;
	int MoveSpeed = 7;
	bool onGround;
	private float pushBackForce = 400;
	// Use this for initialization
	public override void Start () {
		onGround = false;
		base.Start ();
	}

	protected override void OnMove() {
		if (onGround) {
			transform.LookAt (Player);
			rB.AddForce (transform.forward * MoveSpeed);
		}
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.CompareTag("Player")) {
			Body hitPlayer = col.gameObject.GetComponent<Body>();
			hitPlayer.getHit (pushBackForce, rB.transform.position);
		}

		if (col.gameObject.name == "Plane") {
			onGround = true;
		} else {
			onGround = false;
		}
	}
}
