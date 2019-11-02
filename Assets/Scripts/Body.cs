using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MovingObject {
	public float maxSpeed = 5f;
	public float dashSpeed = 300f;
	public GameObject head;

	// Use this for initialization
	public override void Start () {
		head = GameObject.Find ("Head");
		base.Start ();
	}


	protected override void OnMove () {
		float sideMove = Input.GetAxis ("AD-Horizontal");
		float straightMove = Input.GetAxis ("WS-Vertical");
		Vector3 movement = new Vector3 (sideMove, 0.0f, straightMove);

		rB.AddForce (movement * maxSpeed);

		head.transform.position = new Vector3 (transform.position.x, head.transform.position.y, transform.position.z);
	}
}
