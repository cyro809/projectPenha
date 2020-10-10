using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    bool playerOn;
    public float friction;
    PhysicMaterial physicM;
    
    void Start()
    {
        playerOn = false;
        physicM = gameObject.GetComponent<Collider>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Body") && !playerOn) {
            playerOn = true;
            Body playerBody = other.gameObject.GetComponent<Body>();
            playerBody.setAcceleration(physicM.dynamicFriction);
            playerBody.StopPlayer(physicM.dynamicFriction);
        } 
    }

    void OnCollisionExit(Collision other) {
        if(other.gameObject.CompareTag("Body") && playerOn) {
            playerOn = false;
        } 
    }

    void OnCollisionStay(Collision other) {
        if(other.gameObject.CompareTag("Body")) {
            Body playerBody = other.gameObject.GetComponent<Body>();
            playerBody.setAcceleration(friction);
            playerBody.StopPlayer(physicM.dynamicFriction);
        }
    }

    public bool isPlayerOn() {
        return playerOn;
    }

    
}
