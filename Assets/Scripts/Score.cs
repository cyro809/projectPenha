using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private int counter = 0;

	public TextMeshProUGUI countText;
	// Use this for initialization
	void Start () {
		countText = gameObject.GetComponent<TextMeshProUGUI> ();
		SetCountText ();
	}

	public void addPoint(int points) {
		counter += points;
		SetCountText ();
	}

	void SetCountText() {
		countText.text = "Score: " + counter.ToString ();
	}
}
