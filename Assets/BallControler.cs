using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControler : MonoBehaviour {
	public float maxSpeed = 5f;
	public float dashSpeed = 300f;
	Rigidbody rB;

	// Use this for initialization
	void Start () {
		rB = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float sideMove = Input.GetAxis ("AD-Horizontal");
		float straightMove = Input.GetAxis ("WS-Vertical");
		Vector3 movement = new Vector3 (sideMove, 0.0f, straightMove);

		rB.AddForce (movement * maxSpeed);


		if (Input.GetMouseButtonDown (0)) {
			Vector3 sp = Camera.main.WorldToScreenPoint (transform.position);
			Vector3 dir = (Input.mousePosition - sp).normalized;
			dir.y = 0.0f;
			rB.AddForce (dir * dashSpeed);
		}
	}
}
