using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col) {
        if (IsEnemy(col)) {
            Enemy hittedEnemy = col.gameObject.GetComponent<Enemy>();
            hittedEnemy.getHit(3000.0f, transform.position);
        }
    }

    bool IsEnemy(Collision col) {
        return col.gameObject.CompareTag("Enemy") ||
            col.gameObject.CompareTag("ChopperEnemy") ||
            col.gameObject.CompareTag("ShootingEnemy");
    }
}
