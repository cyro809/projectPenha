using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
