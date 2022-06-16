using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject {
	protected GameObject player;
	GameObject scoreGameObject;
	GameState gameState;
	Score score;
	public int scorePoints = 1;
	protected bool enemyTriggered = false;
	protected bool onGround = false;
	public bool spawned = false;
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
		rB = gameObject.GetComponent<Rigidbody>();

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
		if(CanMove()) {
			rB.constraints = RigidbodyConstraints.None;
			Body playerBody = player.GetComponent<Body>();
			if (IsTriggered() && IsPlayerAlive()) {
				Vector3 direction = GetLookAtDirection(player);
				if(onGround) {
					rB.AddForce (direction * MoveSpeed);
				} else {
					AirMove(direction);
				}
			} else if (!playerBody.alive || !IsTriggered() && onGround) {
				rB.constraints = RigidbodyConstraints.FreezeAll;
			}

		}
		if (gameState.IsGameOver()) {
			rB.constraints = RigidbodyConstraints.FreezeAll;
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

	bool CanMove() {
		return (gameState.IsGameRunning() && IsPlayerAlive());
	}

	bool IsPlayerAlive() {
		return player != null && player.GetComponent<Body>() != null;
	}

	protected virtual void AirMove(Vector3 direction) {
		rB.AddForce (direction * airMoveSpeed);
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
		if(IsSpecialEnemy()) {
			Destroy(transform.parent.gameObject);
			Destroy (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	bool IsSpecialEnemy() {
		Transform parent = transform.parent;
		if(parent != null) {
			return parent.gameObject.CompareTag("ShootingEnemy") || parent.gameObject.CompareTag("ChopperEnemy");
		}
		return false;
	}

	protected virtual void OnCollisionEnter (Collision col) {
		if (col.gameObject.CompareTag("Body") || col.gameObject.CompareTag("Player")) {
			HitPlayerIfAlive();
		}

		if (col.gameObject.CompareTag("Ground")) {
			onGround = true;
			if (spawned) {
				TriggerEnemy();
			}
			if(audioSource) {
				ChangeAudioClip();
			}
		}

		if (col.gameObject.CompareTag("Bullet")) {
			if(audioSource) {
				PlayBulletHitSound();
			}
		}

		if (col.gameObject.CompareTag("LimitPlane")) {
			Kill();
		}
	}

	protected void HitPlayerIfAlive() {
		if (IsPlayerAlive()) {
			Body hitPlayer = player.GetComponent<Body>();
			Vector3 pos = new Vector3(rB.transform.position.x, player.transform.position.y, rB.transform.position.z);
			hitPlayer.getHit (pushBackForce, pos);
		}
	}

	protected virtual void OnCollisionExit (Collision col) {
		if (col.gameObject.CompareTag("Ground")) {
			onGround = false;
		}
	}

	protected virtual void PlayBulletHitSound() {
		audioSource.volume = PlayerPrefs.GetFloat("soudEffectsVolume");
        audioSource.Play();
	}

	protected virtual void ChangeAudioClip() {
		audioSource.clip = bulletHit;
        audioSource.loop = false;
	}

	public void ActivateMove() {
		OnMove ();
	}
}
