using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code taken from: https://forum.unity.com/threads/ball-wont-stay-on-moving-platform.530643/
public class EqualizeVelocity : MonoBehaviour
{
    private GameObject target = null;
    private Vector3 offset;
    void Start()
    {
        target = null;
    }
    void OnCollisionStay(Collision col)
    {
        if ((col.transform.tag == "Body"))
        {
            target = col.gameObject;
            offset = target.transform.position - transform.position;
        }
    }
    void OnCollisionExit(Collision col)
    {
        target = null;
    }
    void FixedUpdate()
    {
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }
}
