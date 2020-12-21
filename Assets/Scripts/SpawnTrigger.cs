using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] spawnerObject;
    bool hasEntered = false;

    void OnCollisionEnter (Collision col) {
        if(col.gameObject.CompareTag("Body") && !hasEntered) {
            for (int i=0;i < spawnerObject.Length; i++) {
                if(spawnerObject[i] != null) {
                    spawnerObject[i].GetComponent<Spawner>().TriggerSpawn();
                }
            }
            hasEntered = true;
        }
    }
}
