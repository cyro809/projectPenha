using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float speed;
	public GameObject enemy;
	private float pushBackForce = 1000;
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
			audioSource.Play();
			Enemy hitEnemy = col.gameObject.GetComponent<Enemy>();
			hitEnemy.getHit (pushBackForce, transform.position);
			Destroy (gameObject);
		}
		if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Wall")) {
			Destroy (gameObject);
		}
	}
}
