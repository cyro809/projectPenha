using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private float pushBackForce = 1650;
    public float fadeSpeed = 5/30;
    // Start is called before the first frame update
    public bool activate;
    public GameObject shieldText;
    int count;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color col = gameObject.GetComponent<MeshRenderer> ().material.color;
        col.a -= Time.deltaTime * fadeSpeed;
        GetComponent<MeshRenderer>().material.color = col;
        gameObject.SetActive (activate);
    }

    void OnCollisionEnter (Collision col) {
		if (col.gameObject.CompareTag("Enemy")) {
			Enemy hitEnemy = col.gameObject.GetComponent<Enemy>();
			hitEnemy.getHit (pushBackForce, transform.position);
		}
	}

    public void ActivateShield() {
        Color col = gameObject.GetComponent<MeshRenderer> ().material.color;
        col.a = 1.0f;
        GetComponent<MeshRenderer>().material.color = col;
        activate = true;
        gameObject.SetActive (true);
        StartCoroutine(CountdownEnum(5));
    }

    IEnumerator CountdownEnum(int seconds)
    {
        count += seconds;
        while (count > 0) {
            // display something...
            yield return new WaitForSeconds(1);
            count --;
        }
       
        // count down is finished...
        activate = false;
    }

}
