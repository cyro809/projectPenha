using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public bool isFiring;

	public BulletController bullet;
	public float bulletSpeed;

	public float timeBetweenShots;
	private float shotCounter;
	public float spreadAngle;
	public int spreadAmount;

	bool shotGunMode = false;
	public int shotGunBullets = 10;
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
				// BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as BulletController;
				// newBullet.beFired (bulletSpeed);
				shotGunFire();
				audioSource.Play();
				isFiring = false;
				resetShotCounter();
			}
		} 
	}
	public void resetShotCounter() {
		shotCounter = timeBetweenShots;
	}

	void shotGunFire() {	
		float perBulletAngle = spreadAngle / (spreadAmount - 1);
       	float startAngle = spreadAngle * -0.5f;
  
		for (int i = 0; i < spreadAmount; i++)
		{				
			BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as BulletController;
			newBullet.gameObject.transform.Rotate(Vector3.up, startAngle + i * perBulletAngle);
			newBullet.beFired (bulletSpeed);			
		}
	}

	void setShotGunMode() {
		shotGunMode = true;

	}
}
