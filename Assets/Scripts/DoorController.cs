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
        ToggleDoor();
        if(transform.rotation == openRotation) {
            isOpened = true;
        } else if (transform.rotation == closedRotation) {
            isOpened = false;
        }
    }

    public void ToggleDoor() {
        if(isOpened) {
            target = closedRotation;
        } else {
            target = openRotation;
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, moveSpeed * Time.deltaTime);        
    }

}
