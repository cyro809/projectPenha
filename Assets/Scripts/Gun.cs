using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    protected bool isFiring = false;
    protected int specialBulletsNumber = 0;
     protected float bulletSpeed = 2000;
     protected float timeBetweenShots = 1.0f;
    protected string gunName;
    protected AudioClip shotSound;

    protected virtual void Start()
    {
        shotSound = Resources.Load<AudioClip>("SoundEffects/shoot");
    }
    public virtual void fire(BulletController bullet, Transform firePoint) {
        BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as BulletController;
        newBullet.beFired (bulletSpeed);
    }

    public float getTimeBetweenShots() {
        return timeBetweenShots;
    }

    public string getGunName() {
        return gunName;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
