using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookController : MonoBehaviour {

	private Camera mainCamera;

	public GunController gun;

	// Use this for initialization
	void Start () {
		mainCamera = FindObjectOfType<Camera> ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		Ray cameraRay = mainCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
		float rayLength;

		if (groundPlane.Raycast (cameraRay, out rayLength)) {
			Vector3 pointToLook = cameraRay.GetPoint (rayLength);
			Debug.DrawLine (cameraRay.origin, pointToLook, Color.blue);
			transform.LookAt (pointToLook);
		}

		transform.rotation = Quaternion.Euler (0, transform.eulerAngles.y, transform.eulerAngles.z);

		if (Input.GetMouseButtonDown (0)) {
			gun.isFiring = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			gun.isFiring = false;
		}
	}
}
