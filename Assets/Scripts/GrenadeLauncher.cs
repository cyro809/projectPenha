using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Gun
{
    public override int SpecialBulletsNumber { get { return 5; } }
    public override float TimeBetweenShots { get { return 1.5f; } }
    public override string GunName { get { return "Grenade Launcher"; } }
    public override AudioClip ShotSound { get {return Resources.Load<AudioClip>("SoundEffects/grenade-launcher");} }

    public void Fire(GrenadeController grenade, Transform firePoint) {
        GrenadeController newGrenade = Instantiate (grenade, firePoint.position, firePoint.rotation) as GrenadeController;
        newGrenade.beFired (BulletSpeed);
    }

   public override void SetGunColor(Material gunMaterial) {
        gunMaterial.EnableKeyword("_EMISSION");
        gunMaterial.SetColor("_EmissionColor", new Color(0.6f, 0.4f, 0.0f, 1.0f));
    }
}
