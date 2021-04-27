using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopperEnemy : MonoBehaviour
{
    public float rotationSpeed = 100;
    public GameObject enemyBody;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up *(rotationSpeed * Time.deltaTime));
        Vector3 enemyPosition = enemyBody.transform.position;
        transform.position = new Vector3(enemyPosition.x, transform.position.y, enemyPosition.z);
    }
}
