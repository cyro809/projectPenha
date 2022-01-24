using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public GameObject enemyGun;
    // GameObject player;
    // // Start is called before the first frame update
    // void Start()
    // {
    //     player = GameObject.Find ("Body");
    // }

    // Update is called once per frame
    protected override void OnMove()
    {
        enemyGun.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

        enemyGun.transform.LookAt(player.transform.position);
        base.OnMove();
    }
}
