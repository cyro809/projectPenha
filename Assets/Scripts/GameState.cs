using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
	bool gameOver;
	GameObject gameOverText;
	GameObject button;
	Text text;
	// Use this for initialization
	void Start () {
		gameOver = false;
		gameOverText = GameObject.FindGameObjectWithTag ("GameOverText");
		button = GameObject.FindGameObjectWithTag ("RestartButton");
		button.SetActive (false);
		text = gameOverText.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeStateToGameOverState() {
		gameOver = true;

		setGameOverText ();
	}

	void setGameOverText() {
		if (gameOver) {
			button.SetActive (true);
			text.text = "Game Over!";	
		}

	}

}
