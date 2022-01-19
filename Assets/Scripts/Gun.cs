using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    protected bool isFiring = false;
    protected int specialBulletsNumber {get;} = 0;
     protected float bulletSpeed = 2000;
     protected int bulletsPerShot = 1 ;
     protected float timeBetweenShots = 1.0f;
    protected string gunName;
    protected AudioClip shotSound;

    protected virtual void Start()
    {
        shotSound = Resources.Load<AudioClip>("SoundEffects/shoot");
    }
    public virtual void Fire(BulletController bullet, Transform firePoint) {
        BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as BulletController;
        newBullet.beFired (bulletSpeed);
    }

    public virtual float GetTimeBetweenShots() {
        return timeBetweenShots;
    }

    public virtual string GetGunName() {
        return gunName;
    }

    public int SpecialBulletsNumber{get;}

    public virtual int GetBulletsPerShot() {
        return bulletsPerShot;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
