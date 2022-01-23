using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunController : GunController
{


    // Update is called once per frame
    protected override void Update () {
		shotCounter -= Time.deltaTime;

		if (isFiring && IsGameStarted()) {
			if (shotCounter <= 0) {
				shotCounter = gun.TimeBetweenShots;
				gun.Fire(bullet, firePoint);

				resetShotCounter();
			}
		}
	}

    bool CanFireGun() {
		GameObject parentObj = transform.parent.gameObject;
        if(parentObj.tag == "ShootingEnemy") {
            Enemy enemyComponent = FindEnemyComponent(parentObj);
            return enemyComponent.IsTriggered();
        }
        return true;
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
