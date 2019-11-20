using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
	bool gameOver;
	GameObject gameOverText;
	GameObject button;
	GameObject joystick;
	Text text;
	// Use this for initialization
	void Start () {
		gameOver = false;
		gameOverText = GameObject.FindGameObjectWithTag ("GameOverText");
		button = GameObject.FindGameObjectWithTag ("RestartButton");
		button.SetActive (false);
		text = gameOverText.GetComponent<Text> ();
		joystick = GameObject.FindGameObjectWithTag ("Joystick");

		if (SystemInfo.deviceType != DeviceType.Handheld) {
			joystick.SetActive (false);
		}
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
