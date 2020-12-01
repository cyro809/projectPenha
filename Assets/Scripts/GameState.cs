using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
	public bool gameOver = false;
	public bool gameStart = false;
	public bool gameWin = false;
	public bool paused;
	public TextMeshProUGUI gameOverText;
	public GameObject restartButton;
	public GameObject titleButton;
	public GameObject nextLevelButton;
	GameObject joystick;
	Text text;
	GameObject countDownText;
	Countdown countdownObj;
	GameObject backGroundThemeObj;
	public GameObject pauseCanvasObject;
	Canvas pauseCanvas;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 40;
		gameOver = false;
		paused = false;
		pauseCanvas = pauseCanvasObject.GetComponent<Canvas>();
		gameOverText.enabled = false;
		countDownText = GameObject.FindGameObjectWithTag("CountDownText");
		countdownObj = countDownText.GetComponent<Countdown>();
		restartButton.SetActive (false);
		titleButton.SetActive(false);
		joystick = GameObject.FindGameObjectWithTag ("Joystick");
		backGroundThemeObj = GameObject.FindGameObjectWithTag("BackgroundTheme");

		if (SystemInfo.deviceType != DeviceType.Handheld) {
			//joystick.SetActive (false);
		}
	}

	void FixedUpdate() {
		if(countDownText != null && countdownObj != null && countdownObj.finishCount) {
			changeStateToGameStartState();
		}

	}

	void Update() {
		TogglePause();
	}

	public void changeStateToGameOverState() {
		gameStart = false;
		gameOver = true;
		gameWin = false;
		setGameOverText ();
		playGameOverMusic();
	}

	public void changeStateToWinState() {
		gameStart = false;
		gameOver = true;
		gameWin = true;
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
		if (gameOver && !gameWin) {
			restartButton.SetActive (true);
			titleButton.SetActive(true);
			gameOverText.enabled = true;
			gameOverText.text = "Game Over!";
		}
	}

	void setWinText() {
		if (gameOver && gameWin) {
			nextLevelButton.SetActive (true);
			titleButton.SetActive(true);
			gameOverText.enabled = true;
			gameOverText.text = "Great!";
		}

	}

	void playGameOverMusic() {
		BackgroundTheme theme = GetBackgroundTheme();
		theme.ToggleToGameOverSound();
	}

	void TogglePause() {
		if(gameStart) {
			if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
				paused = !paused;
				if(paused) {
					Time.timeScale = 0;

				} else if(!paused) {
					Time.timeScale = 1;
				}
				pauseCanvas.enabled = paused;
			}
		}

	}
}
