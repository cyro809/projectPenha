using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float speed;

	public float pushBackForce = 1000;
	public float lightEnemyPushBackForce = 1500;
	public float heavyEnemyPushBackForce = 200;

	public float playerPushBackForce = 2000;
	Rigidbody rb;
	AudioSource audioSource;

	void Start() {
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefs.GetFloat("soudEffectsVolume");
	}
	void OnBecameInvisible() {
		Destroy (gameObject);
	}

	public void beFired(float speed) {
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (transform.forward * speed);
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.CompareTag("Enemy")) {
			HitEnemy(pushBackForce, col);
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
			Destroy (gameObject);
		}

		if(col.gameObject.CompareTag("Body") || col.gameObject.CompareTag("Player")) {
			HitPlayer(playerPushBackForce, col);
		}
	}

	void HitEnemy(float pushForce, Collision col) {
		if(gameObject.CompareTag("Bullet")) {
			Enemy hitEnemy = col.gameObject.GetComponent<Enemy>();
			if (hitEnemy.IsTriggered()) {
				hitEnemy.getHit (pushForce, transform.position);
				audioSource.Play();
			}
			Destroy (gameObject);
		}
		
	}

	void HitPlayer(float pushForce, Collision col) {
		if(gameObject.CompareTag("CannonBall") || gameObject.CompareTag("ShootingEnemyBullet")) {
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
