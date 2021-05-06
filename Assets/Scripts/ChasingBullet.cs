using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBullet : MonoBehaviour
{
    public Transform target;
    public float pushBackForce = 1000;
	public float speed = 1000f;
	public float rotateSpeed = 200f;

	private Rigidbody rb;
    bool lockedOn = false;

	// Use this for initialization
	void Start () {

	}

	void FixedUpdate () {
        if(target && target != null) {
            rb = gameObject.AddComponent<Rigidbody>() as Rigidbody;
            Debug.Log(target);
            Vector3 direction = (Vector3)target.position - rb.position;

            direction.Normalize();
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);

            rb.velocity = transform.up * speed;
        }

	}

    void OnCollisionEnter (Collision col) {
		if (col.gameObject.CompareTag("Enemy")) {
			HitEnemy(pushBackForce, col);
		}
        Debug.Log(col.gameObject);
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

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
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
        return closest;
    }
}
