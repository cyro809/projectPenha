using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : Enemy
{
    public float scale;
    int scaleSpeed = 100;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        SetScale();
    }


    protected override void OnCollisionEnter (Collision col) {

		if (col.gameObject.CompareTag("Bullet")) {
			scale -= 1f;
            if( scale > 1) {
                SetScale();
                SetPropeties();
            }
		}
	}

    void SetScale() {
        transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(scale, scale, scale), scaleSpeed * Time.deltaTime);

    }

    void SetPropeties() {
        pushBackForce += 50;
        MoveSpeed += 5;
        rB.mass -= 2;
    }
}
