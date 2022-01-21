using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserBulletController : BulletController
{

    Transform target;
    public float rotateSpeed = 1000f;
    bool lockedOn = false;
    public Material chaserMaterial;

    // Update is called once per frame
    void Update () {
        if(lockedOn && target != null) {
            Vector3 direction = (Vector3)target.position - rb.position;

            direction.Normalize();
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);

            rb.velocity = transform.forward * speed;
        }
	}

    public override void beFired(float speed) {
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (transform.forward * speed);
        target = FindClosestEnemy();
        if(target != null) lockedOn = true;
	}

    Transform FindClosestEnemy()
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
