using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Body : MovingObject {
	public float maxSpeed;
	float defaultMaxSpeed;
    public float dashSpeed = 300f;
	public float acceleration = 2f;
	public float jumpForce = 20f;
	public float jumpAcceleration = 30f;
	float groundFriction = 0 ;
	float defaultAcceleration;
	public bool alive;
	private bool isGrounded;
	GameObject head;
	public GameObject shield;
	public GameObject gun;
	GameObject plane;
	GameObject gameOverText;
	GameState gameState;
	AudioSource audioSource;
	AudioClip BulletHit  { get {return Resources.Load<AudioClip>("SoundEffects/player-bullet-hit");} }
	public TextMeshProUGUI powerUpText;



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
		alive = true;
		isGrounded = false;
		defaultAcceleration = acceleration;
		defaultMaxSpeed = maxSpeed;
	}


	protected override void OnMove () {
		if(gameState.IsGameRunning()) {
			float sideMove = Input.GetAxisRaw ("AD-Horizontal");
			float straightMove = Input.GetAxisRaw ("WS-Vertical");
			float rigidBodyMagnitude = rB.velocity.magnitude;

			if (rigidBodyMagnitude < maxSpeed) {
				movement = new Vector3 (sideMove, 0.0f, straightMove);
				rB.AddForce (movement * GetAcceleration());
			}

			if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
				rB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

	float GetAcceleration() {
		if (isGrounded) {
			return acceleration;
		} else {
			return jumpAcceleration;
		}
	}

	public float getDefaultAcceleration() {
		return defaultAcceleration;
	}

	public float getDefaultMaxSpeed() {
		return defaultMaxSpeed;
	}

	public void setAcceleration(float friction) {
		maxSpeed = defaultMaxSpeed - friction;
		rB.velocity = Vector3.ClampMagnitude(rB.velocity, maxSpeed);
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

	public void FreezePlayer() {
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
			audioSource.volume = PlayerPrefs.GetFloat("soudEffectsVolume");
			audioSource.Play();
		}
		if (col.gameObject.CompareTag("EnemyBullet")) {
			audioSource.PlayOneShot(BulletHit, 0.7F);
		}
		if (col.gameObject.CompareTag("Goal")) {
			ChangeToWinState();
		}

		if(col.gameObject.CompareTag("ShieldPowerUp")) {
			shield.GetComponent<Shield>().ActivateShield();
			StartCoroutine(showText("Shield"));
		}

		if(col.gameObject.layer == LayerMask.NameToLayer("PowerUp") && !col.gameObject.CompareTag("ShieldPowerUp")) {
			GunController gunController = gun.GetComponent<GunController>();
			gunController.SetGunMode(col.gameObject.tag);
			StartCoroutine(showText(gunController.GetGunName()));
		}

		if(col.gameObject.CompareTag("Ground")) {
			groundFriction = col.gameObject.GetComponent<Ground>().friction;
			isGrounded = true;
		}

	}
	void OnCollisionExit(Collision col) {
		if(col.gameObject.CompareTag("Ground")) {
			isGrounded = false;
		}
	}

	void OnCollisionStay(Collision col) {
		if (col.gameObject.CompareTag("LimitPlane")) {
			KillPlayer();
		}

		if(col.gameObject.CompareTag("Ground")) {
			isGrounded = true;
		}
	}

	IEnumerator showText(string text) {
        powerUpText.text = text;
        // display something...
        yield return new WaitForSeconds(1);
		powerUpText.text = "";

    }
}
