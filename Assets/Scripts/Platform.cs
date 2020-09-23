using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    Quaternion target;
    bool isRotating;

    void Start()
    {
        isRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRotating) {
            Rotate();
        }

        if(transform.rotation == target) {
            isRotating = false;
        }
    }

    public void SetTarget(Quaternion newTarget) {
        target = newTarget;
    }

    void Rotate() {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, moveSpeed * Time.deltaTime);
    }

    public void ActivateRotation() {
        isRotating = true;
    }
}
