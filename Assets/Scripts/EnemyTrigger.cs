using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemiesObject;

    void OnCollisionEnter (Collision col) {
        if(col.gameObject.CompareTag("Body")) {
            for (int i=0;i < enemiesObject.Length; i++) {
                enemiesObject[i].GetComponent<Enemy>().TriggerEnemy();
            }
        }
    }
}
