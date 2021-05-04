using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBulletRadar : MonoBehaviour
{
    // Start is called before the first frame update
    public ChasingBullet chasingBullet;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision col) {
        if(col.gameObject.CompareTag("Enemy")) {
            if(!chasingBullet.IsLockedOn()) {
                chasingBullet.target = col.gameObject.transform;
                chasingBullet.SetLockOn();
            }

        }
    }
}
