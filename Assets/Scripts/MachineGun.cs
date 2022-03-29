using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Gun
{
    public override int SpecialBulletsNumber { get { return 50; } }
    public override float TimeBetweenShots { get { return 0.1f; } }
    public override string GunName { get { return "Machine Gun"; } }
    public override AudioClip ShotSound { get {return Resources.Load<AudioClip>("SoundEffects/machinegun");} }

    // Update is called once per frame
    void Update()
    {

    }
}
