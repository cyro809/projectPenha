using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GunController : MonoBehaviour {
	const int NORMAL_GUN_MODE = 0;
	const int SHOT_GUN_MODE = 1;
	const int MACHINE_GUN_MODE = 2;
	
	public bool isFiring;

	public BulletController bullet;
	public float bulletSpeed;

	public float timeBetweenShots;
	private float shotCounter;
	public float spreadAngle;
	public int spreadAmount;
	int gunMode = 0;
	bool shotGunMode = false;
	bool machineGunMode = true;
	public int specialShotBullets = 0;
	public Text shotCounterText;
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
				if(gunMode == SHOT_GUN_MODE) {
					shotGunFire();
				} else {
					normalFire();
				}
				
				audioSource.Play();
				if(!machineGunMode) {
					isFiring = false;
				}
				
				resetShotCounter();
			}
		}
		if (specialShotBullets <= 0) {
			ResetGunMode(); 
		}
		showShotCounterText();
	}
	public void resetShotCounter() {
		shotCounter = timeBetweenShots;
	}

	void normalFire() {
		BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as BulletController;
		newBullet.beFired (bulletSpeed);
		if(gunMode == MACHINE_GUN_MODE) {
			specialShotBullets--;
		}
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
		specialShotBullets -= 3;
	}

	public void setShotGunMode() {
		specialShotBullets = 30;
		gunMode = SHOT_GUN_MODE;
		timeBetweenShots = 1.2f;
	}

	public void setMachineGunMode() {
		specialShotBullets = 50;
		gunMode = MACHINE_GUN_MODE;
		timeBetweenShots = 0.1f;
	}

	void ResetGunMode() {
		gunMode = NORMAL_GUN_MODE;
		timeBetweenShots = 1;
	}

	void showShotCounterText() {
		shotCounterText.text =  "Special Shots: " + specialShotBullets.ToString ();
	}
}
