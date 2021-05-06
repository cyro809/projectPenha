﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject enemyGun;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find ("Body");
    }

    // Update is called once per frame
    void Update()
    {
        enemyGun.transform.position = new Vector3 (transform.position.x, enemyGun.transform.position.y, transform.position.z);

        enemyGun.transform.LookAt(player.transform.position);
    }
}