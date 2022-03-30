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
    Rigidbody rb;
    AudioSource audioSource;
    void Start()
    {
        bombExplosionTransform = transform.GetChild(0);
        explosionRadiusStart = bombExplosionTransform.localScale;
        audioSource = GetComponent<AudioSource>();
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
            if(explosionSpeed >= 1) {
                exploding = false;
                StartCoroutine(WaitBeforeDestroyExplosion(1));
            }
        }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Ground")) {
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            renderer.enabled = false;
            rb = gameObject.GetComponent<Rigidbody>();
            Destroy(rb);
            CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
            collider.enabled = false;
            exploding = true;
            audioSource.Play();
        }
    }

    public virtual void beFired(float speed) {
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (transform.forward * speed);
	}

    IEnumerator WaitBeforeDestroyExplosion(int seconds) {
        int count = seconds;

        while (count > 0) {
            // display something...
            yield return new WaitForSeconds(1);
            count --;
        }

        // count down is finished...
        Destroy(gameObject);
    }
}
