using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBullet : MonoBehaviour
{
    public Transform target;
    public float pushBackForce = 1000;
	public float speed = 5f;
	public float rotateSpeed = 200f;

	private Rigidbody rb;
    bool lockedOn = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
        if(target != null) {
            Vector3 direction = (Vector3)target.position - rb.position;
            Debug.Log(direction);
            direction.Normalize();
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);

            rb.velocity = transform.up * this.GetComponent<BulletController>().speed;
        }

	}

    public void beFired(float speed) {
		rb.AddForce (transform.forward * speed);
	}

    void OnCollisionEnter (Collision col) {
		if (col.gameObject.CompareTag("Enemy")) {
			HitEnemy(pushBackForce, col);
		}
	}

	void HitEnemy(float pushForce, Collision col) {
		if(gameObject.CompareTag("Bullet")) {
			Enemy hitEnemy = col.gameObject.GetComponent<Enemy>();
			if (hitEnemy.IsTriggered()) {
				hitEnemy.getHit (pushForce, transform.position);
			}
			Destroy (gameObject);
		}

	}

    public void SetLockOn() {
        lockedOn = true;
    }

    public bool IsLockedOn() {
        return lockedOn;
    }
}
