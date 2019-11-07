using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject {
	GameObject player;
	GameObject plane;
	int MoveSpeed = 7;
	bool onGround;
	private float pushBackForce = 400;
	// Use this for initialization
	public override void Start () {
		player = GameObject.FindWithTag ("Player");
		plane = GameObject.FindWithTag ("Ground");
		onGround = false;
		base.Start ();
	}

	protected override void OnMove() {
		if (onGround) {
			transform.LookAt (player.transform.position);
			rB.AddForce (transform.forward * MoveSpeed);
		}
		checkIfOutOfArena ();

	}

	void checkIfOutOfArena() {
		if (transform.position.y < plane.transform.position.y - 5)
			Destroy (gameObject);
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.CompareTag("Player")) {
			Body hitPlayer = col.gameObject.GetComponent<Body>();
			hitPlayer.getHit (pushBackForce, rB.transform.position);
		}

		if (col.gameObject.CompareTag("Ground")) {
			onGround = true;
		}
	}

	public void ActivateMove() {
		OnMove ();
	}
}
