using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GunController : MonoBehaviour {
	const int NORMAL_GUN_MODE = 0;
	const int SHOT_GUN_MODE = 1;
	const int MACHINE_GUN_MODE = 2;
	public bool playerGun = true;
	public bool isFiring;

	public BulletController bullet;
	public float bulletSpeed;

	public float timeBetweenShots;
	private float shotCounter;
	public float spreadAngle;
	public int spreadAmount;
	int gunMode = 0;
	public int specialShotBullets = 0;
	public TextMeshProUGUI shotCounterText;
	public Transform firePoint;
	AudioSource audioSource;

	GameState gameState;


	void Start() {
		shotCounter = 0;
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefs.GetFloat("soudEffectsVolume");
		gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();
	}
	// Update is called once per frame
	void Update () {
		shotCounter -= Time.deltaTime;

		if (isFiring && IsGameStarted()) {
			if (shotCounter <= 0) {
				shotCounter = timeBetweenShots;
				if(gunMode == SHOT_GUN_MODE) {
					shotGunFire();
				} else {
					normalFire();
				}

				audioSource.Play();
				if(gunMode == MACHINE_GUN_MODE) {
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
		if(playerGun) {
			switch(gunMode) {
				case NORMAL_GUN_MODE:
					shotCounterText.text = "";
					break;
				case MACHINE_GUN_MODE:
					shotCounterText.text = "Machine Gun: ";
					break;
				case SHOT_GUN_MODE:
					shotCounterText.text = "Shot Gun: ";
					break;
				default:
					shotCounterText.text = "";
					break;
			}
			if (gunMode != NORMAL_GUN_MODE) {
				shotCounterText.text += specialShotBullets.ToString ();
			}
		}

	}

	bool IsGameStarted() {
		return gameState.gameStart && !gameState.paused;
	}
}
