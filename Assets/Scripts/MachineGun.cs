using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Gun
{
    public override int SpecialBulletsNumber { get { return 50; } }
    public override float TimeBetweenShots { get { return 0.1f; } }
    public override string GunName { get { return "Machine Gun"; } }

    // Update is called once per frame
    void Update()
    {

    }
}
