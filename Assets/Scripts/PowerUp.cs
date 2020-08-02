using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Body")) {
             Destroy(gameObject);
        }
    }
}
