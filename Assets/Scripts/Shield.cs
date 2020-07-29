using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private float pushBackForce = 1650;
    private float transparency = 0.1f;
    // Start is called before the first frame update
    
    public bool activate;
    public GameObject shieldText;
    TextMesh countDownText;
    void Start()
    {
        countDownText = shieldText.GetComponent<TextMesh> ();
    }

    // Update is called once per frame
    void Update()
    {
        Color col = gameObject.GetComponent<MeshRenderer> ().material.color;
        col.a = transparency;
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
        activate = true;
        gameObject.SetActive (true);
        StartCoroutine(CountdownEnum(3));
    }

    IEnumerator CountdownEnum(int seconds)
    {
        int count = seconds;
       countDownText = shieldText.GetComponent<TextMesh> ();
        while (count > 0) {
            countDownText.text = "" + count.ToString ();
            // display something...
            yield return new WaitForSeconds(1);
            count --;
        }
       
        // count down is finished...
        activate = false;
    }

}
