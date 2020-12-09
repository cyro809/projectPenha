using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float speed;
	public GameObject enemy;
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
		if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Obstacle") || col.gameObject.CompareTag("Goal")) {
			Destroy (gameObject);
		}
	}

	void HitEnemy(float pushForce, Collision col) {
		Enemy hitEnemy = col.gameObject.GetComponent<Enemy>();
		if (hitEnemy.IsTriggered()) {
			hitEnemy.getHit (pushForce, transform.position);
			audioSource.Play();
		}
		Destroy (gameObject);
	}
}
