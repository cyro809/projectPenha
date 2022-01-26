using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButtonController : MonoBehaviour
{
    public GameObject canvasGameObject;
    public GameObject gameStateObject;
    public void ResumeGame() {
		Time.timeScale = 1;
        canvasGameObject.GetComponent<Canvas>().enabled = false;
        gameStateObject.GetComponent<GameState>().gamePaused = false;
	}
}
