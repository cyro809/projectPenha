using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemiesObject;
    bool hasEntered = false;

    void OnCollisionEnter (Collision col) {
        if(col.gameObject.CompareTag("Body") && !hasEntered) {
            for (int i=0;i < enemiesObject.Length; i++) {
                if(enemiesObject[i] != null) {
                    Enemy enemyComponent = GetEnemyComponent(enemiesObject[i]);
                    enemyComponent.TriggerEnemy();
                }
            }
            hasEntered = true;
        }
    }

    Enemy GetEnemyComponent(GameObject enemyObj) {
        Enemy enemyComponent = enemyObj.GetComponent<Enemy>();
        if(EnemyComponentExists(enemyComponent)) {
            return enemyComponent;
        } else {
            foreach (Transform child in enemyObj.transform) {
                enemyComponent = child.gameObject.GetComponent<Enemy>();
                if(EnemyComponentExists(enemyComponent)) {
                    return enemyComponent;
                }
            }
        }
        return enemyComponent;
    }

    bool EnemyComponentExists(Enemy enemyComponent) {
        return enemyComponent && enemyComponent != null;
    }
}
