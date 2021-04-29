using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    // Start is called before the first frame update
    public float explosionRadius = 7;
    public float explosionSpeed = 0.2f;
    Transform bombExplosionTransform;
    Vector3 explosionRadiusStart;
    bool exploding = false;
    void Start()
    {
        bombExplosionTransform = transform.GetChild(0);
        explosionRadiusStart = bombExplosionTransform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(exploding) {
            bombExplosionTransform.localScale = Vector3.Lerp(
                    explosionRadiusStart,
                    new Vector3(explosionRadius, explosionRadius, explosionRadius),
                explosionSpeed
            );
            explosionSpeed += Time.deltaTime;
            if(explosionSpeed == 1) {
                exploding = false;
            }
        }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Ground")) {
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            renderer.enabled = false;
            Rigidbody rB = gameObject.GetComponent<Rigidbody>();
            Destroy(rB);
            CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
            collider.enabled = false;
            exploding = true;

        }
    }
}
