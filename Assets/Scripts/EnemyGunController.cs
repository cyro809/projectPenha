﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunController : GunController
{


    // Update is called once per frame
    protected override void Update () {
		shotCounter -= Time.deltaTime;

		if (isFiring && IsGameStarted() && CanFireGun()) {
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
            ShootingEnemy enemyComponent = FindEnemyComponent(parentObj);
            return enemyComponent.IsTriggered();
        }
		return false;
	}

	ShootingEnemy FindEnemyComponent(GameObject enemyObj) {
		ShootingEnemy enemyComponent = null;
		foreach (Transform child in enemyObj.transform) {
			enemyComponent = child.gameObject.GetComponent<ShootingEnemy>();
			if(EnemyComponentExists(enemyComponent)) {
				return enemyComponent;
			}
		}
		return enemyComponent;
	}

	bool EnemyComponentExists(ShootingEnemy enemyComponent) {
        return enemyComponent && enemyComponent != null;
    }
}
