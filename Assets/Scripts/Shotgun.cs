using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    protected new int specialBulletsNumber = 30;
    protected new float timeBetweenShots = 1.2f;
    protected new int bulletsPerShot = 3;
    protected new string gunName = "Shotgun";
    float spreadAngle = 30.0f;
    // Start is called before the first frame update
    // void Start()
    // {

    // }

    public override void Fire(BulletController bullet, Transform firePoint) {
        float perBulletAngle = spreadAngle / (bulletsPerShot - 1);
       	float startAngle = spreadAngle * -0.5f;

		for (int i = 0; i < bulletsPerShot; i++)
		{
			BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as BulletController;
			newBullet.gameObject.transform.Rotate(Vector3.up, startAngle + i * perBulletAngle);
			newBullet.beFired (bulletSpeed);
		}
    }

    public new int SpecialBulletsNumber {get { return specialBulletsNumber; }}

    // Update is called once per frame
    void Update()
    {

    }
}
