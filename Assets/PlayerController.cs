using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	public GameObject body;

	void Start () {
		body = GameObject.Find ("Body");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector3 (body.transform.position.x, 0.0f, body.transform.position.z);
	}
}
