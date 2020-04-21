using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public bool isFiring;

	public BulletController bullet;
	public float bulletSpeed;

	public float timeBetweenShots;
	private float shotCounter;

	public Transform firePoint;
	AudioSource audioSource;
	

	void Start() {
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefs.GetFloat("soudEffectsVolume");
	}
	// Update is called once per frame
	void Update () {
		if (isFiring) {
			shotCounter -= Time.deltaTime;
			if (shotCounter <= 0) {
				shotCounter = timeBetweenShots;
				BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as BulletController;
				newBullet.beFired (bulletSpeed);
				audioSource.Play();
			}
		} else {
			shotCounter = 0;
		}
	}
}
