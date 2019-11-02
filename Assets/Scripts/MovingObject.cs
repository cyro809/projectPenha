using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {
	
	protected Rigidbody rB;
	// Use this for initialization
	public virtual void Start () {
		rB = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		OnMove ();
	}

	protected virtual void OnMove () {
		
	}

	public void getHit (float magnitude, Vector3 colliderPosition) {
		Vector3 force = transform.position - colliderPosition;
		force.Normalize ();
		rB.AddForce (force * magnitude);
	}
}

