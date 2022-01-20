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
	public float bulletSpeed;

	public GrenadeController grenade;

	public float timeBetweenShots;
	private float shotCounter;
	public float spreadAngle;
	public int spreadAmount;
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
				gun.Fire(bullet, firePoint);
				if(!isNormalGun()) {
					specialShotBullets -= gun.BulletsPerShot;
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
		shotCounter = gun.TimeBetweenShots;
	}

	void normalFire() {
		if(gunMode == NORMAL_GUN_MODE || gunMode == CHASING_BULLET_MODE || gunMode == MACHINE_GUN_MODE) {
			BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as BulletController;
			if(gunMode == CHASING_BULLET_MODE) {
				newBullet.SetChaserBullet();
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

	public void setShotGunMode() {
		gunMode = SHOT_GUN_MODE;
		Destroy(gameObject.GetComponent<Gun>());
		gun = gameObject.AddComponent<Shotgun>();
		specialShotBullets = gun.SpecialBulletsNumber;
	}

	public void setMachineGunMode() {
		specialShotBullets = 50;
		gunMode = MACHINE_GUN_MODE;
		timeBetweenShots = 0.1f;
	}

	public void setGrenadeLauncherMode() {
		specialShotBullets = 5;
		gunMode = GRENADE_LAUNCHER_MODE;
		timeBetweenShots = 1.5f;
	}

	public void setChasingBulletMode() {
		specialShotBullets = 10;
		gunMode = CHASING_BULLET_MODE;
		timeBetweenShots = 1;
	}

	void ResetGunMode() {
		Destroy(gun);
		gun = gameObject.AddComponent<Gun>();
		gunMode = NORMAL_GUN_MODE;
	}

	bool isNormalGun() {
		return gunMode == NORMAL_GUN_MODE;
	}

	void showShotCounterText() {

		if(playerGun) {
			TextMeshProUGUI shotCounterText = GameObject.FindWithTag("ShotCounter").GetComponent<TextMeshProUGUI>();
			shotCounterText.text = gun.GunName;
			// switch(gunMode) {
			// 	case NORMAL_GUN_MODE:
			// 		shotCounterText.text = "";
			// 		break;
			// 	case MACHINE_GUN_MODE:
			// 		shotCounterText.text = "Machine Gun: ";
			// 		break;
			// 	case SHOT_GUN_MODE:
			// 		shotCounterText.text = "Shot Gun: ";
			// 		break;
			// 	case CHASING_BULLET_MODE:
			// 		shotCounterText.text = "Chasing BUllets: ";
			// 		break;
			// 	case GRENADE_LAUNCHER_MODE:
			// 		shotCounterText.text = "Grenade Launcher: ";
			// 		break;
			// 	default:
			// 		shotCounterText.text = "";
			// 		break;
			// }
			if (!isNormalGun()) {
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
