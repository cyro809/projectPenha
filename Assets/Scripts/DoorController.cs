using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    Quaternion target;
    public float targetDegree;
    
    void Start()
    {
        target = transform.rotation;
        target = Quaternion.Euler(0, targetDegree, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, moveSpeed * Time.deltaTime);
    }

    void RotateDoor(float degrees) {
        
    }
}
