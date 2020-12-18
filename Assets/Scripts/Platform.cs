using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    float shakeAmount;
    float shakeSpeed;
    public Quaternion targetRotation;

    public Vector3 targetPosition;
    Quaternion originalRotation;
    Quaternion finalRotation;
    bool isRotating;
    bool isMoving;
    bool isAtTheEnd;
    Vector3 originalPosition;

    Vector3 finalPosition;
    public bool toRotate;
    public bool toMove;
    public bool autoMove;
    public int idleCount;



    void Start()
    {
        isAtTheEnd = false;
        shakeAmount = 0.1f;
        shakeSpeed = 200;
        originalPosition = transform.localPosition;
        finalPosition = targetPosition;
        originalRotation = transform.rotation;
        finalRotation = targetRotation;

        AutoMoveIfActivated();
    }

    // Update is called once per frame
    void Update()
    {
        if(toRotate) {
            HandleRotation();
        }

        if(toMove) {
            HandleMove();
        }
    }

    void HandleRotation() {
        if(isRotating) {
            Rotate();
        }

        if(transform.rotation == targetRotation && !isAtTheEnd) {
            isAtTheEnd = true;
            finalRotation = originalRotation;
            isRotating = false;
            AutoMoveIfActivated();
        } else if (transform.rotation == originalRotation && isAtTheEnd) {
            isAtTheEnd = false;

            finalRotation = targetRotation;
            isRotating = false;
            AutoMoveIfActivated();
        }
    }

    void HandleMove() {
        if(isMoving) {
            Move();
        }

        if(transform.localPosition == targetPosition && !isAtTheEnd) {
            isMoving = false;
            isAtTheEnd = true;
            finalPosition = originalPosition;
            AutoMoveIfActivated();

        } else if (transform.localPosition == originalPosition && isAtTheEnd) {
            isMoving = false;
            isAtTheEnd = false;
            finalPosition = targetPosition;
            AutoMoveIfActivated();
        }
    }

    void AutoMoveIfActivated() {
        if(autoMove) {
            StartCoroutine(CountdownEnum(idleCount));
        }
    }

    public void SetTarget(Quaternion newTarget) {
        targetRotation = newTarget;
    }

    public void Rotate() {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, finalRotation, moveSpeed * Time.deltaTime);
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
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, finalPosition, moveSpeed * Time.deltaTime);
    }

    bool isMoveFisinhed() {
        return transform.localPosition == targetPosition && isMoving;
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
