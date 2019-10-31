using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControler : MovingObject {
	public float maxSpeed = 5f;
	public float dashSpeed = 300f;
	Rigidbody rB;
	public GameObject head;

	// Use this for initialization
	void Start () {
		rB = GetComponent<Rigidbody> ();
		head = GameObject.Find ("Head");
	}


	protected override void OnMove () {
		float sideMove = Input.GetAxis ("AD-Horizontal");
		float straightMove = Input.GetAxis ("WS-Vertical");
		Vector3 movement = new Vector3 (sideMove, 0.0f, straightMove);

		rB.AddForce (movement * maxSpeed);

		head.transform.position = new Vector3 (transform.position.x, head.transform.position.y, transform.position.z);
	}

	void OnCollisionEnter (Collision col) {
		
	}

	public void getHit (float magnitude, Vector3 colliderPosition) {
		Vector3 force = transform.position - colliderPosition;
		force.Normalize ();
		rB.AddForce (force * magnitude);
	}
}
