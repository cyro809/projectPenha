using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GunController : MonoBehaviour {
	const int NORMAL_GUN_MODE = 0;
	const int SHOT_GUN_MODE = 1;
	const int MACHINE_GUN_MODE = 2;
	const int GRENADE_LAUNCHER_MODE = 3;
	const int CHASING_BULLET_MODE = 4;
	public bool playerGun = true;
	public bool isFiring;

	public BulletController bullet;
	public ChaserBulletController chaserBullet;
	public float bulletSpeed;

	public GrenadeController grenade;

	public float timeBetweenShots;
	private float shotCounter;
	int gunMode = 0;
	public int specialShotBullets = 0;
	public Transform firePoint;
	AudioSource audioSource;

	GameState gameState;
	Gun gun;


	void Start() {
		shotCounter = 0;
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefs.GetFloat("soudEffectsVolume");
		gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();
		gun = gameObject.AddComponent<Gun>();
	}
	// Update is called once per frame
	void Update () {
		shotCounter -= Time.deltaTime;

		if (isFiring && IsGameStarted() && CanFireGun()) {
			if (shotCounter <= 0) {
				shotCounter = gun.TimeBetweenShots;
				if(IsGrenadeLauncherGun()) {
					((GrenadeLauncher)gun).Fire(grenade, firePoint);
				}
				else if(!IsChaserGun()) {
					gun.Fire(bullet, firePoint);
				}
				else {
					gun.Fire(chaserBullet, firePoint);
				}

				if(!IsNormalGun()) {
					specialShotBullets -= gun.BulletsPerShot;
				}

				audioSource.Play();
				MachineGunIsFiringReset();

				resetShotCounter();
			}
		}
		if (specialShotBullets <= 0) {
			ResetGunMode();
		}
		showShotCounterText();
	}

	void MachineGunIsFiringReset() {
		if(gunMode == MACHINE_GUN_MODE) {
			isFiring = false;
		}
	}
	public void resetShotCounter() {
		shotCounter = gun.TimeBetweenShots;
	}

	void normalFire() {
		if(gunMode == NORMAL_GUN_MODE || gunMode == CHASING_BULLET_MODE || gunMode == MACHINE_GUN_MODE) {
			BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as BulletController;
			if(gunMode == CHASING_BULLET_MODE) {

			}
			newBullet.beFired (bulletSpeed);

		} else if (gunMode == GRENADE_LAUNCHER_MODE) {
			GrenadeController newGrenade = Instantiate (grenade, firePoint.position, firePoint.rotation) as GrenadeController;
			newGrenade.beFired (bulletSpeed);
		}

		if(gunMode == MACHINE_GUN_MODE || gunMode == GRENADE_LAUNCHER_MODE || gunMode == CHASING_BULLET_MODE) {
			specialShotBullets--;
		}
	}

	public void SetGunMode(string powerUpTag) {
		Destroy(gun);

		switch(powerUpTag) {
			case "ShotGunPowerUp":
				SetShotGunMode();
				break;
			case "MachineGunPowerUp":
				SetMachineGunMode();
				break;
			case "ChasingBulletPowerUp":
				SetChasingBulletMode();
				break;
			case "GrenadePowerUp":
				SetGrenadeLauncherMode();
				break;
			default:
				ResetGunMode();
				break;
		}

		SetGunSpecs();
	}

	public string GetGunName() {
		return gun.GunName;
	}

	public void SetShotGunMode() {
		gunMode = SHOT_GUN_MODE;
		gun = gameObject.AddComponent<Shotgun>();
	}

	public void SetMachineGunMode() {
		gunMode = MACHINE_GUN_MODE;
		gun = gameObject.AddComponent<MachineGun>();
	}



	void SetGunSpecs() {
		specialShotBullets = gun.SpecialBulletsNumber;
		timeBetweenShots = gun.TimeBetweenShots;
	}

	public void SetGrenadeLauncherMode() {
		gunMode = GRENADE_LAUNCHER_MODE;
		gun = gameObject.AddComponent<GrenadeLauncher>();
		Debug.Log("SetGrenadeLauncher");
	}

	public void SetChasingBulletMode() {
		gunMode = CHASING_BULLET_MODE;
		gun = gameObject.AddComponent<ChaserGun>();

		SetGunSpecs();
	}

	void ResetGunMode() {
		Destroy(gun);
		gunMode = NORMAL_GUN_MODE;
		gun = gameObject.AddComponent<Gun>();
		SetGunSpecs();
	}

	bool IsNormalGun() {
		return gunMode == NORMAL_GUN_MODE;
	}

	bool IsChaserGun() {
		return gunMode == CHASING_BULLET_MODE;
	}

	bool IsGrenadeLauncherGun() {
		return gunMode == GRENADE_LAUNCHER_MODE;
	}

	void showShotCounterText() {
		if(playerGun) {
			TextMeshProUGUI shotCounterText = GameObject.FindWithTag("ShotCounter").GetComponent<TextMeshProUGUI>();
			shotCounterText.text = gun.GunName;
			if (!IsNormalGun()) {
				shotCounterText.text += ": " + specialShotBullets.ToString ();
			}
		}
	}

	bool IsGameStarted() {
		return gameState.gameStart && !gameState.paused;
	}

	bool CanFireGun() {
		if(!playerGun) {
			GameObject parentObj = transform.parent.gameObject;
			if(parentObj.tag == "ShootingEnemy") {
				Enemy enemyComponent = FindEnemyComponent(parentObj);
				return enemyComponent.IsTriggered();
			}
			return true;
		} else {
			return true;
		}
	}

	Enemy FindEnemyComponent(GameObject enemyObj) {
		Enemy enemyComponent = null;
		foreach (Transform child in enemyObj.transform) {
			enemyComponent = child.gameObject.GetComponent<Enemy>();
			if(EnemyComponentExists(enemyComponent)) {
				return enemyComponent;
			}
		}
		return enemyComponent;
	}

	bool EnemyComponentExists(Enemy enemyComponent) {
        return enemyComponent && enemyComponent != null;
    }
}
