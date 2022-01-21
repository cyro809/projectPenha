using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float speed = 40;

	public float pushBackForce = 1000;
	public float lightEnemyPushBackForce = 1500;
	public float heavyEnemyPushBackForce = 200;
	public float playerPushBackForce = 2000;
	protected Rigidbody rb;
	protected AudioSource audioSource;


	protected virtual void Start() {
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefs.GetFloat("soudEffectsVolume");
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}

	public virtual void beFired(float speed) {
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (transform.forward * speed);
	}

	protected virtual void OnCollisionEnter (Collision col) {
		if (col.gameObject.CompareTag("Enemy")) {
			HitEnemy(pushBackForce, col);
		}
		if (col.gameObject.CompareTag("ShootingEnemyBoss")) {
			HitBoss(500, col);
		}
		if (col.gameObject.CompareTag("LightEnemy")) {
			HitEnemy(lightEnemyPushBackForce, col);
		}
		if (col.gameObject.CompareTag("HeavyEnemy")) {
			HitEnemy(heavyEnemyPushBackForce, col);
		}
		if (col.gameObject.CompareTag("Ground") ||
		col.gameObject.CompareTag("Wall") ||
		col.gameObject.CompareTag("Obstacle") ||
		col.gameObject.CompareTag("Goal") ||
		(col.gameObject.CompareTag("Cannon") && gameObject.CompareTag("Bullet"))) {
			Debug.Log("Bullet collide!");
			Destroy (gameObject);
		}

		if(col.gameObject.CompareTag("Body") || col.gameObject.CompareTag("Player")) {
			HitPlayer(playerPushBackForce, col);
		}

	}

	protected virtual void HitEnemy(float pushForce, Collision col) {
		if(gameObject.CompareTag("Bullet")) {
			Enemy hitEnemy = col.gameObject.GetComponent<Enemy>();
			if (hitEnemy.IsTriggered()) {
				hitEnemy.getHit (pushForce, transform.position);
				// audioSource.Play();
			}
			Destroy (gameObject);
		}

	}

	void HitBoss(float pushForce, Collision col) {
		if(gameObject.CompareTag("Bullet")) {
			Patrol hitEnemy = col.gameObject.GetComponent<Patrol>();
			hitEnemy.getHit (pushForce, transform.position);
			Destroy (gameObject);
		}

	}

	void HitPlayer(float pushForce, Collision col) {
		if(gameObject.CompareTag("CannonBall") || gameObject.CompareTag("EnemyBullet")) {
			Body player = col.gameObject.GetComponent<Body>();
			if(player) {
				Vector3 pos = new Vector3(rb.transform.position.x, player.transform.position.y, rb.transform.position.z);
				player.getHit (pushForce, pos);
				audioSource.Play();
			}
			Destroy (gameObject);
		}

	}

}
