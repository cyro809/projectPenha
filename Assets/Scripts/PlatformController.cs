using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject platformGameObject;
    void Start()
    {
        Platform platform = platformGameObject.GetComponent<Platform>();
        platform.SetTarget(Quaternion.Euler(0, 0, 30));
        platform.ActivateRotation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
