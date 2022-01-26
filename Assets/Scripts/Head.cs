using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

	private Camera mainCamera;

	public GunController gun;

	GameState gameState;

	// Use this for initialization
	void Start () {
		mainCamera = FindObjectOfType<Camera> ();
		Input.multiTouchEnabled = true;
		gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();
	}

	// Update is called once per frame
	void Update () {
		if(gameState.gameStart && !gameState.gameOver && !gameState.gamePaused) {
			LookAtMouseDirection();

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

	void LookAtMouseDirection() {
		// Player facing mouse Reference: https://www.youtube.com/watch?v=E56-ekpz0rM
			Vector3 firePos = Input.mousePosition;
			Ray cameraRay = UnityEngine.Camera.main.ScreenPointToRay (firePos);
			Plane playerPlane = new Plane (Vector3.up, transform.position);
			float rayLength;

			if (playerPlane.Raycast (cameraRay, out rayLength)) {
				Vector3 pointToLook = cameraRay.GetPoint (rayLength);
				Debug.DrawLine (transform.position, pointToLook, Color.blue);
				transform.LookAt (pointToLook);
			}

			transform.rotation = Quaternion.Euler (0, transform.eulerAngles.y, 0);
	}

	bool DetectFireInput() {
		if (Input.GetMouseButton (0)) {
			return true;
		}
		return false;
	}

	bool DetectFireInputEnd() {
		if (Input.GetMouseButtonUp (0)) {
			return true;
		}
		return false;
	}
}
