using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private int counter = 0;

	public Text countText;
	// Use this for initialization
	void Start () {
		countText = gameObject.GetComponent<Text> ();
		SetCountText ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void restartCounter() {
		counter = 0;
	}

	public void addPoint() {
		counter++;
		SetCountText ();
	}

	void SetCountText() {
		countText.text = "Score: " + counter.ToString ();
	}
}
