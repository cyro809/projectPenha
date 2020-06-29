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
		shotCounter = 0;
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefs.GetFloat("soudEffectsVolume");
	}
	// Update is called once per frame
	void Update () {
		shotCounter -= Time.deltaTime;
		if (isFiring) {
			if (shotCounter <= 0) {
				shotCounter = timeBetweenShots;
				BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as BulletController;
				newBullet.beFired (bulletSpeed);
				audioSource.Play();
				isFiring = false;
				resetShotCounter();
			}
		} 
	}
	public void resetShotCounter() {
		shotCounter = timeBetweenShots;
	}
}
