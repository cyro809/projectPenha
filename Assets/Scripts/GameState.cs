using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
	bool gameOver;
	public bool gameStart = false;
	GameObject gameOverText;
	public GameObject button;
	GameObject joystick;
	Text text;
	GameObject countDownText;
	Countdown countdownObj;
	GameObject backGroundThemeObj;
	// Use this for initialization
	void Start () {
		gameOver = false;
		countDownText = GameObject.FindGameObjectWithTag("CountDownText");
		countdownObj = countDownText.GetComponent<Countdown>();
		gameOverText = GameObject.FindGameObjectWithTag ("GameOverText");
		button.SetActive (false);
		text = gameOverText.GetComponent<Text> ();
		joystick = GameObject.FindGameObjectWithTag ("Joystick");
		backGroundThemeObj = GameObject.FindGameObjectWithTag("BackgroundTheme");

		if (SystemInfo.deviceType != DeviceType.Handheld) {
			joystick.SetActive (false);
		}
	}

	void FixedUpdate() {
		if(countDownText != null && countdownObj != null && countdownObj.finishCount) {
			changeStateToGameStartState();
		}
	}

	public void changeStateToGameOverState() {
		gameOver = true;
		setGameOverText ();
		playGameOverMusic();
	}

	public void changeStateToWinState() {
		gameOver = true;
		setWinText ();
	}

	void changeStateToGameStartState() {
		gameStart = true;
		countDownText.SetActive(false);
	}

	BackgroundTheme GetBackgroundTheme() {
		return backGroundThemeObj.GetComponent<BackgroundTheme>();
	}

	void setGameOverText() {
		if (gameOver) {
			button.SetActive (true);
			text.text = "Game Over!";	
		}
	}

	void setWinText() {
		if (gameOver) {
			button.SetActive (true);
			text.text = "Great!";	
		}

	}

	void playGameOverMusic() {
		BackgroundTheme theme = GetBackgroundTheme();
		theme.ToggleToGameOverSound();
	}

}
