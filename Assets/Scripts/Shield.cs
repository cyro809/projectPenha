using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private float pushBackForce = 2050;
    public float fadeSpeed = 5/30;
    // Start is called before the first frame update
    public bool activate;
    public TextMeshProUGUI shieldText;
    int count;
    AudioClip ShieldHit  { get {return Resources.Load<AudioClip>("SoundEffects/shield-hit");} }
    AudioSource audioSource;

    Coroutine currentCoroutine = null;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
		if (col.gameObject.CompareTag("Enemy") ||
            col.gameObject.CompareTag("LightEnemy") ||
            col.gameObject.CompareTag("HeavyEnemy")) {
			Enemy hitEnemy = col.gameObject.GetComponent<Enemy>();
			hitEnemy.getHit (pushBackForce, transform.position);
            audioSource.PlayOneShot(ShieldHit, 0.7F);
		}
	}

    public void ActivateShield() {
        Color col = gameObject.GetComponent<MeshRenderer> ().material.color;
        col.a = 1.0f;
        GetComponent<MeshRenderer>().material.color = col;
        activate = true;
        gameObject.SetActive (true);
        if(currentCoroutine != null) {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ShieldCountDown(5));
    }

    IEnumerator ShieldCountDown(int seconds)
    {
        count = seconds;
        while (count > 0) {
            // display something...
            DisplayShieldCounter();
            yield return new WaitForSeconds(1);
            count --;
        }

        // count down is finished...
        activate = false;
    }

    void DisplayShieldCounter() {
        shieldText.text = count.ToString();
    }

}
