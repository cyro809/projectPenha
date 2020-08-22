using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject {
	GameObject player;
	GameObject plane;
	GameObject scoreGameObject;
	GameState gameState;
	Score score;
	bool onGround;
	public int MoveSpeed;
	public float pushBackForce;
	AudioSource audioSource;
	AudioClip bulletHit;

	// Use this for initialization
	public override void Start () {
		player = GameObject.Find ("Body");
		plane = GameObject.FindWithTag ("Ground");

		scoreGameObject = GameObject.FindWithTag ("Score");
		score = scoreGameObject.GetComponent<Score> ();

		gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();

		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefs.GetFloat("soudEffectsVolume");
		
		bulletHit = Resources.Load<AudioClip>("SoundEffects/enemy-hit");
		onGround = false;
		base.Start ();
	}

	protected override void OnMove() {
		GetPlayerReference();
		if(IsGameStartedAndIsPlayerAlive()) {
			rB.constraints = RigidbodyConstraints.None;
			Body playerBody = player.GetComponent<Body>();
			if (onGround && player != null) {
				Vector3 direction = GetLookAtDirection(player);
				rB.AddForce (direction * MoveSpeed);

			} else if (!playerBody.alive) {
				rB.constraints = RigidbodyConstraints.FreezeAll;
			}
			
		} 
		if (gameState.gameOver) {
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

	bool IsGameStartedAndIsPlayerAlive() {
		return (gameState.gameStart && player != null && player.GetComponent<Body>() != null);
	}

	void GetPlayerReference() {
		player = GameObject.Find ("Body");
	}

	void Kill() {
		score.addPoint ();
		Destroy (gameObject);
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.CompareTag("Body")) {
			if (player.GetComponent<Body>() != null) {
				Body hitPlayer = col.gameObject.GetComponent<Body>();
				hitPlayer.getHit (pushBackForce, rB.transform.position);
			}
			
		}
		
		if (col.gameObject.CompareTag("Ground")) {
			onGround = true;
			ChangeAudioClip();
		}

		if (col.gameObject.CompareTag("Bullet")) {
			PlayBulletHitSound();
		}

		if (col.gameObject.CompareTag("LimitPlane")) {
			Kill();
		}
	}

	void OnCollisionStay(Collision col) {
		if (col.gameObject.CompareTag("LimitPlane")) {
			Kill();
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
