using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitableObject : MonoBehaviour
{
   public float pushBackForce;
   public bool destroyableObject = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col) {
        if(IsPlayer(col)) {
			HitPlayer(col);
		}
        if(col.gameObject.CompareTag("Bullet") && destroyableObject) {
            Destroy(gameObject);
        }
    }

    void HitPlayer(Collision col) {
        Body player = col.gameObject.GetComponent<Body>();
        if(player) {
            Vector3 pos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            player.getHit (pushBackForce, pos);
        }
    }

    bool IsPlayer(Collision col) {
        return col.gameObject.CompareTag("Body") || col.gameObject.CompareTag("Player");
    }
}
