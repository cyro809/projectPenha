using System;
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

	bool chaser = false;
	Transform target;
	public float rotateSpeed = 1000f;
    bool lockedOn = false;

	public Material chaserMaterial;
	public Material defaultMaterial;


	void Start() {
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefs.GetFloat("soudEffectsVolume");
	}


	void Update () {
        if(IsChaserBullet()) {
            Vector3 direction = (Vector3)target.position - rb.position;

            direction.Normalize();
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);

            rb.velocity = transform.forward * speed;
        }

	}

	bool IsChaserBullet() {
		return chaser && target && target != null && lockedOn;
	}

	public void SetChaserBullet() {
		chaser = true;
		gameObject.GetComponent<MeshRenderer>().material = chaserMaterial;
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}

	public void beFired(float speed) {
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (transform.forward * speed);
		if(chaser) {
			target = FindClosestEnemy();
			if(target != null) lockedOn = true;
		}
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

	public Transform FindClosestEnemy()
    {
        GameObject[] gos;
		GameObject[] lightEnemies;
		GameObject[] enemies;
		GameObject[] heavyEnemies;
		lightEnemies = GameObject.FindGameObjectsWithTag("LightEnemy");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        heavyEnemies = GameObject.FindGameObjectsWithTag("HeavyEnemy");
		int enemiesArraySize = lightEnemies.Length + enemies.Length + heavyEnemies.Length;
        gos = new GameObject[enemiesArraySize];

		Array.Copy(lightEnemies, 0, gos, 0, lightEnemies.Length);
		Array.Copy(enemies, 0, gos, lightEnemies.Length, enemies.Length);
		Array.Copy(heavyEnemies, 0, gos, lightEnemies.Length + enemies.Length, heavyEnemies.Length);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
			Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
		if(closest != null) {
        	return closest.transform;
		}
		return null;
    }
}
