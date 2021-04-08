using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float speed;
	private float pushBackForce = 1000;
	private float lightEnemyPushBackForce = 1500;
	private float heavyEnemyPushBackForce = 200;
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
		Debug.Log(transform.forward);
		rb.AddForce (transform.forward * speed);
		Debug.Log(rb.velocity.magnitude);
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
		if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Obstacle") || col.gameObject.CompareTag("Goal")) {
			Destroy (gameObject);
		}
		if(col.gameObject.CompareTag("Body") || col.gameObject.CompareTag("Player")) {
			HitPlayer(pushBackForce, col);
		}
	}

	void HitEnemy(float pushForce, Collision col) {
		if(gameObject.CompareTag("Bullet")) {
			Enemy hitEnemy = col.gameObject.GetComponent<Enemy>();
			if (hitEnemy.IsTriggered()) {
				hitEnemy.getHit (pushForce, transform.position);
				audioSource.Play();
			}
		}
		Destroy (gameObject);
	}

	void HitPlayer(float pushForce, Collision col) {
		if(gameObject.CompareTag("CannonBall")) {
			Body player = col.gameObject.GetComponent<Body>();
			player.getHit (pushForce, transform.position);
			audioSource.Play();
		}
		Destroy (gameObject);
	}
}
