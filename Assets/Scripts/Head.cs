using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

	private Camera mainCamera;

	public GunController gun;
	Vector3 firePos;
	GameState gameState;

	// Use this for initialization
	void Start () {
		mainCamera = FindObjectOfType<Camera> ();
		Input.multiTouchEnabled = true;
		gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();
	}

	// Update is called once per frame
	void Update () {
		if(gameState.gameStart) {
			if (SystemInfo.deviceType == DeviceType.Handheld) {
				firePos = Input.GetTouch (TouchNumber()).position;
			} else {
				firePos = Input.mousePosition;
			}
			Ray cameraRay = mainCamera.ScreenPointToRay (firePos);
			Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
			float rayLength;

			if (groundPlane.Raycast (cameraRay, out rayLength)) {
				Vector3 pointToLook = cameraRay.GetPoint (rayLength);
				Debug.DrawLine (cameraRay.origin, pointToLook, Color.blue);
				transform.LookAt (pointToLook);
			}

			transform.rotation = Quaternion.Euler (0, transform.eulerAngles.y, 0);

			if (DetectFireInput()) {
				gun.isFiring = true;
			} else {
				gun.isFiring = false;
			}

			if (DetectFireInputEnd()) {
				gun.isFiring = false;
			}
		}
		
	}

	bool DetectFireInput() {
		if (SystemInfo.deviceType == DeviceType.Handheld) {
			if (Input.GetTouch (TouchNumber()).phase == TouchPhase.Began) {
				return true;
			}
			return false;
		} else {
			if (Input.GetMouseButtonDown (0)) {
				return true;
			}
			return false;
		}
	}

	bool DetectFireInputEnd() {
		if (SystemInfo.deviceType == DeviceType.Handheld) {
			if (Input.GetTouch (TouchNumber()).phase == TouchPhase.Ended) {
				return true;
			}
			return false;
		} else {
			if (Input.GetMouseButtonUp (0)) {
				return true;
			}
			return false;
		}
	}

	int TouchNumber() {
		if (Input.touchCount == 1) {
			return 0;
		}

		return 1;
	}
}
