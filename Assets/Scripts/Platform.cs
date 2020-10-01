using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public float shakeAmount;
    public float shakeSpeed;
    Quaternion target;
    bool isRotating;

    void Start()
    {
        isRotating = false;
        shakeSpeed = 200;
        shakeAmount = 0.1f;
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
        Shake();
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
}
