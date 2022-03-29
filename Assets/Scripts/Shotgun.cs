using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public override int SpecialBulletsNumber { get { return 30; } }
    public override int BulletsPerShot { get { return 3; } }
    public override float TimeBetweenShots { get { return 1.2f; } }
    public override string GunName { get { return "Shotgun"; } }
    float spreadAngle = 30.0f;
    public override AudioClip ShotSound { get {return Resources.Load<AudioClip>("SoundEffects/shotgun");} }



    public override void Fire(BulletController bullet, Transform firePoint) {
        float perBulletAngle = spreadAngle / (BulletsPerShot - 1);
       	float startAngle = spreadAngle * -0.5f;

		for (int i = 0; i < BulletsPerShot; i++)
		{
			BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as BulletController;
			newBullet.gameObject.transform.Rotate(Vector3.up, startAngle + i * perBulletAngle);
			newBullet.beFired (BulletSpeed);
		}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
