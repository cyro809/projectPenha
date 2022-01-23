using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Gun
{
    public override int SpecialBulletsNumber { get { return 5; } }
    public override float TimeBetweenShots { get { return 1.5f; } }
    public override string GunName { get { return "Grenade Launcher"; } }

    public void Fire(GrenadeController grenade, Transform firePoint) {
        GrenadeController newGrenade = Instantiate (grenade, firePoint.position, firePoint.rotation) as GrenadeController;
        newGrenade.beFired (BulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
