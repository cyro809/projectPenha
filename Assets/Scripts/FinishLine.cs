using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color col = gameObject.GetComponent<MeshRenderer> ().material.color;
        col.a = 0.5f;
        GetComponent<MeshRenderer>().material.color = col;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
