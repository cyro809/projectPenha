using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : MonoBehaviour
{
    public float scale;
    int scaleSpeed = 100;
    Enemy enemyComponent;
    // Start is called before the first frame update
    void Start()
    {
        SetScale();
        enemyComponent = gameObject.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter (Collision col) {

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
        enemyComponent.pushBackForce += 50;
        enemyComponent.MoveSpeed += 5;
        gameObject.GetComponent<Rigidbody>().mass -= 2;
    }
}
