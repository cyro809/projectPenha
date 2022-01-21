using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    protected bool isFiring = false;
    public virtual int SpecialBulletsNumber { get { return 0; } }
    protected virtual float BulletSpeed { get { return 2000.0f; } }
    public virtual int BulletsPerShot { get { return 1; } }
    public virtual float TimeBetweenShots { get { return 1.0f; } }
    public virtual string GunName {get; }
    protected AudioClip shotSound;

    protected virtual void Start()
    {
        shotSound = Resources.Load<AudioClip>("SoundEffects/shoot");
    }
    public virtual void Fire(BulletController bullet, Transform firePoint) {
        Debug.Log("Gun Fire");
        BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as BulletController;
        newBullet.beFired (BulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
