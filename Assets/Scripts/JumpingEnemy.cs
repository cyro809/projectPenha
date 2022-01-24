using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : Enemy
{
    public float jumpForce = 20f;

    protected override void AirMove(Vector3 direction) {
		if (!onGround) {
			rB.AddForce (direction * MoveSpeed);
		}
	}

    void Jump() {
		if(onGround) {
			rB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

    protected override void OnCollisionEnter (Collision col) {
		OnGroundCollision(col);
        base.OnCollisionEnter(col);
	}
    void OnGroundCollision(Collision col) {
        if (col.gameObject.CompareTag("Ground")) {
			onGround = true;
            Jump();
		}
    }
}
