using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public float dropSpeed;
    float shakeAmount;
    float shakeSpeed;
    Quaternion target;
    bool isRotating;
    bool isMoving;
    Vector3 originalPosition;
    public Vector3 targetPosition;
    public bool toRotate;
    public bool toMove;
    public bool autoMove;
    public int idleCount;

    void Start()
    {
        isRotating = false;
        shakeAmount = 0.1f;
        shakeSpeed = 200;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(toRotate) {
            if(isRotating) {
                Rotate();
            }

            if(transform.rotation == target) {
                isRotating = false;
                AutoMoveIfActivated();
            }
        }

        if(toMove) {
            if(isMoving) {
                Move();
            }

            if(transform.position == targetPosition) {
                isMoving = false;
                AutoMoveIfActivated();
            }
        }

    }

    void AutoMoveIfActivated() {
        if(autoMove) {
            StartCoroutine(CountdownEnum(idleCount));
        }
    }

    public void SetTarget(Quaternion newTarget) {
        target = newTarget;
    }

    public void Rotate() {
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

    public void ActivateMovement() {
        isMoving = true;
    }

    public void Move() {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    IEnumerator CountdownEnum(int seconds)
    {
        int count = seconds;

        while (count > 0) {

            // display something...
            yield return new WaitForSeconds(1);
            count --;
        }

        // count down is finished...
        if (toMove) {
            ActivateMovement();
        }
        if (toRotate) {
            ActivateRotation();
        }
    }
}
