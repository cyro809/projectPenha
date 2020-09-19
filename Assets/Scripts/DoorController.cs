using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    Quaternion openRotation;
    Quaternion closedRotation;
    Quaternion target;

    public float openedDegree;
    bool isOpened;
    public bool isActive;
    void Start()
    {
        openRotation = Quaternion.Euler(0, openedDegree, 0);
        closedRotation = transform.rotation;
        target = openRotation;
        isOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive) {
            ToggleDoor();
        }
        if(transform.rotation == openRotation) {
            isOpened = true;
            isActive = false;
        } else if (transform.rotation == closedRotation) {
            isOpened = false;
            isActive = false;
        }
    }

    void ToggleDoor() {
        if(isOpened) {
            target = closedRotation;
        } else {
            target = openRotation;
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, moveSpeed * Time.deltaTime);        
    }
    public void ActivateDoor() {
        isActive = true;
    }
}
