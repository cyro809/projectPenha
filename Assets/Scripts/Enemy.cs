using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject {
	GameObject player;
	GameObject scoreGameObject;
	GameState gameState;
	Score score;
	public int scorePoints = 1;
	bool enemyTriggered = false;
	bool onGround = false;
	public bool spawned = false;
	public bool jumpingEnemy;
	public float jumpForce = 20f;

	public int MoveSpeed;
	int airMoveSpeed = 10;
	public float pushBackForce;
	AudioSource audioSource;
	AudioClip bulletHit;
	string gameMode;
	GameObject gameTypeObject;
	GameType gameType;

	// Use this for initialization
	public override void Start () {
		player = GameObject.Find ("Body");

		scoreGameObject = GameObject.FindWithTag ("Score");
		score = scoreGameObject.GetComponent<Score> ();

		gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();

		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefs.GetFloat("soudEffectsVolume");

		bulletHit = Resources.Load<AudioClip>("SoundEffects/enemy-hit");
		gameTypeObject = GameObject.Find("GameTypeObject");
		gameType = gameTypeObject.GetComponent<GameType>();
		gameMode = gameType.GetGameMode();
		base.Start ();
	}

	protected override void OnMove() {
		GetPlayerReference();
		rB = gameObject.GetComponent<Rigidbody>();
		if(IsGameStartedAndIsPlayerAlive()) {
			rB.constraints = RigidbodyConstraints.None;
			Body playerBody = player.GetComponent<Body>();
			if (enemyTriggered && player !=  null) {
				Vector3 direction = GetLookAtDirection(player);
				if(onGround || (!onGround && jumpingEnemy)) {
					rB.AddForce (direction * MoveSpeed);
				} else if (!onGround && !jumpingEnemy) {
					rB.AddForce (direction * airMoveSpeed);
				}

			} else if (!playerBody.alive || !IsTriggered() && onGround) {
				rB.constraints = RigidbodyConstraints.FreezeAll;
			}

		}
		if (gameState.gameOver) {
			rB.constraints = RigidbodyConstraints.FreezeAll;
		}

	}

	void Jump() {
		if(onGround) {
			rB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

	Vector3 GetLookAtDirection(GameObject player) {
		/*
		Code reference from: https://answers.unity.com/questions/239614/roll-a-ball-towards-the-player.html
		*/

		Vector3 targetPosition = player.transform.position;
		Vector3 myPosition = transform.position;

		// work out direction and distance
		return (targetPosition - myPosition).normalized;
	}

	bool IsGameStartedAndIsPlayerAlive() {
		return (gameState.gameStart  && !gameState.paused && player != null && player.GetComponent<Body>() != null);
	}

	public void TriggerEnemy() {
		enemyTriggered = true;
	}

	public bool IsTriggered() {
		return enemyTriggered;
	}

	void GetPlayerReference() {
		player = GameObject.Find ("Body");
	}

	void Kill() {
		score.addPoint (scorePoints);
		Destroy (gameObject);
	}

	void OnCollisionEnter (Collision col) {

		if (col.gameObject.CompareTag("Body") || col.gameObject.CompareTag("Player")) {
			if (player.GetComponent<Body>() != null) {
				Body hitPlayer = player.GetComponent<Body>();
				Vector3 pos = new Vector3(rB.transform.position.x, player.transform.position.y, rB.transform.position.z);
				hitPlayer.getHit (pushBackForce, pos);
			}

		}

		if (col.gameObject.CompareTag("Ground")) {
			onGround = true;
			if (spawned) {
				TriggerEnemy();
			}
			if(jumpingEnemy) {
				Jump();
			}
			ChangeAudioClip();
		}

		if (col.gameObject.CompareTag("Bullet")) {
			PlayBulletHitSound();
		}

		if (col.gameObject.CompareTag("LimitPlane")) {
			Kill();
		}
	}

	void OnCollisionExit (Collision col) {
		if (col.gameObject.CompareTag("Ground")) {
			onGround = false;
		}
	}

	void OnCollisionStay(Collision col) {
		if (col.gameObject.CompareTag("LimitPlane")) {
			Kill();
		}

		if (col.gameObject.CompareTag("Ground")) {
			onGround = true;
			if(jumpingEnemy) {
				Jump();
			}
			ChangeAudioClip();
		}
	}

	void PlayBulletHitSound() {
        audioSource.Play();
	}

	void ChangeAudioClip() {
		audioSource.clip = bulletHit;
        audioSource.loop = false;
	}

	public void ActivateMove() {
		OnMove ();
	}
}
