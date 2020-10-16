 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Body : MovingObject {
	public float maxSpeed = 5f;
    public float dashSpeed = 300f;
	public float acceleration = 2f;
	float groundFriction = 0 ;
	float defaultAcceleration;
	public bool alive;
	GameObject head;
	public GameObject shield;
	public GameObject gun;
	GameObject plane;
	GameObject gameOverText;
	SimpleTouchController joystick;
	GameState gameState;
	AudioSource audioSource;
	public Text powerUpText;

	Vector3 movement;
	

	// Use this for initialization
	public override void Start () {
		head = GameObject.Find ("Head");
		
		plane = GameObject.FindWithTag ("Ground");
		
		gameOverText = GameObject.FindWithTag ("GameOverText"); 
		gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();

		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefs.GetFloat("soudEffectsVolume");

		base.Start ();
		joystick = FindObjectOfType<SimpleTouchController> ();
		alive = true;
		defaultAcceleration = acceleration;
	}


	protected override void OnMove () {
		if(gameState.gameStart) {
			float sideMove = Input.GetAxisRaw ("AD-Horizontal");
			float straightMove = Input.GetAxisRaw ("WS-Vertical");	
			float rigidBodyMagnitude = rB.velocity.magnitude;

			if (SystemInfo.deviceType == DeviceType.Handheld) {
				sideMove = joystick.GetTouchPosition.x;
				straightMove = joystick.GetTouchPosition.y;
			}
			
			if (rigidBodyMagnitude < maxSpeed) {
				movement = new Vector3 (sideMove, 0.0f, straightMove);
				rB.AddForce (movement * acceleration);
			}
		
			head.transform.position = new Vector3 (transform.position.x, head.transform.position.y, transform.position.z);
			if(shield != null) {
				shield.transform.position = new Vector3 (transform.position.x, head.transform.position.y, transform.position.z);
			}
			
		}
		
	}

	bool hasNoInput(float sideMove, float straightMove) {
		return sideMove == 0 && straightMove == 0;
	}

	public float getDefaultAcceleration() {
		return defaultAcceleration;
	}

	public void setAcceleration(float friction) {
		rB.AddForce(movement * (-friction * 50 ));
	}

	public void SetDrag(float friction) {
		rB.angularDrag = friction;
		rB.drag = friction;
	}

	void KillPlayer() {
		alive = false;
		FreezePlayer();
		gameState.changeStateToGameOverState ();
		gameObject.SetActive(false);
		head.gameObject.SetActive(false);
	}

	void FreezePlayer() {
		GameObject parent = transform.parent.gameObject;
		Rigidbody parentRigidBody = parent.GetComponent<Rigidbody>();
		parentRigidBody.constraints = RigidbodyConstraints.FreezeAll;
		rB.constraints = RigidbodyConstraints.FreezeAll;
	}

	void ChangeToWinState() {
		FreezePlayer();
		gameState.changeStateToWinState ();
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag("LimitPlane")) {
			KillPlayer();
		}
		if (col.gameObject.CompareTag("Enemy")) {
			audioSource.Play();
		} 	
		if (col.gameObject.CompareTag("Goal")) {
			ChangeToWinState();
		}
		if(col.gameObject.CompareTag("ShieldPowerUp")) {
			shield.GetComponent<Shield>().ActivateShield();
			StartCoroutine(showText("Shield"));
		}
		if(col.gameObject.CompareTag("ShotGunPowerUp")) {
			gun.GetComponent<GunController>().setShotGunMode();
			StartCoroutine(showText("Shot Gun"));
		}
		if(col.gameObject.CompareTag("Ground")) {
			groundFriction = col.gameObject.GetComponent<Ground>().friction;
		}
	}

	void OnCollisionStay(Collision col) {
		if (col.gameObject.CompareTag("LimitPlane")) {
			KillPlayer();
		}
	}

	IEnumerator showText(string text) {
        powerUpText.text = text;
        // display something...
        yield return new WaitForSeconds(1);
		powerUpText.text = "";
       
    }
}
