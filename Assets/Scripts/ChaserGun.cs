using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserGun : Gun
{
    public override int SpecialBulletsNumber { get { return 10; } }

    public override string GunName { get { return "Chaser Gun"; } }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }

    public void Fire(ChaserBulletController bullet, Transform firePoint) {
        ChaserBulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as ChaserBulletController;
        newBullet.beFired (BulletSpeed);
    }
}
