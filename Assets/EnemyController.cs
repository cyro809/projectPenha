using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MovingObject {
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
		if (col.gameObject.name == "Body") {
			BallControler hitPlayer = col.gameObject.GetComponent<BallControler>();
			hitPlayer.getHit (pushBackForce, rB.transform.position);
		}

		if (col.gameObject.name == "Plane") {
			onGround = true;
		} else {
			onGround = false;
		}
	}
}
