using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    float shakeAmount;
    float shakeSpeed;
    Quaternion target;
    Vector3 dropTarget;
    bool isRotating;

    void Start()
    {
        isRotating = false;
        shakeAmount = 0.1f;
        shakeSpeed = 200;
        dropTarget = new Vector3(transform.position.x, -7.5f, transform.position.z);
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

    void Shake() {
        Vector3 position = transform.position;
        position.x = position.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
        transform.position = position;
    }

    void Drop() {
        transform.position = Vector3.MoveTowards(transform.position, dropTarget, moveSpeed * Time.deltaTime);
    }
}
