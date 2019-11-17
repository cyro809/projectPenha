using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject {
	GameObject player;
	GameObject plane;
	GameObject scoreGameObject;
	Score score;
	bool onGround;
	public int MoveSpeed;
	public float pushBackForce;

	// Use this for initialization
	public override void Start () {
		player = GameObject.FindWithTag ("Player");
		plane = GameObject.FindWithTag ("Ground");
		scoreGameObject = GameObject.FindWithTag ("Score");
		score = scoreGameObject.GetComponent<Score> ();
		onGround = false;
		base.Start ();
	}

	protected override void OnMove() {
		if (onGround && player != null) {
			transform.LookAt (player.transform.position);
			rB.AddForce (transform.forward * MoveSpeed);
		} else if (player == null) {
			rB.constraints = RigidbodyConstraints.FreezeAll;
		}
		checkIfOutOfArena ();
	}

	void checkIfOutOfArena() {
		if (transform.position.y < plane.transform.position.y - 5) {
			score.addPoint ();
			Destroy (gameObject);
		}
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
