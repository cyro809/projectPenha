using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    bool playerOn;
    void Start()
    {
        playerOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player") && !playerOn) {
            playerOn = true;
        } 
    }

    void OnCollisionExit(Collision other) {
        if(other.gameObject.CompareTag("Player") && playerOn) {
            playerOn = false;
        } 
    }

    public bool isPlayerOn() {
        return playerOn;
    }
    
}
