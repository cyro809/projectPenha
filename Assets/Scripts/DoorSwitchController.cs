using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitchController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door;
    

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.CompareTag("Body")) {
            DoorController doorController = door.GetComponent<DoorController>();
            doorController.ActivateDoor();
        }
    }
}
