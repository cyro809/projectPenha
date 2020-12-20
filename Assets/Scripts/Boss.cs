using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject gameStateObject;
    public GameObject playerBodyGameObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter (Collision col) {
		if (col.gameObject.CompareTag("LimitPlane")) {
			gameStateObject.GetComponent<GameState>().changeStateToWinState();
            playerBodyGameObject.GetComponent<Body>().FreezePlayer();
		}
	}
}
