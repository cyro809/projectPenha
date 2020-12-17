﻿using System.Collections;
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
                    enemiesObject[i].GetComponent<Enemy>().TriggerEnemy();
                }
            }
            hasEntered = true;
        }
    }
}