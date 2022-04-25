using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : Gun
{
    protected override float BulletSpeed { get { return 1000.0f; } }
    public override float TimeBetweenShots { get { return 3.0f; } }

}
