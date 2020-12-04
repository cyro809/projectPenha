using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameHandler : MonoBehaviour
{
    public void StartGameScene() {
        string gameMode = PlayerPrefs.GetString("gameMode", "mainGame");
        Scene scene = SceneManager.GetActiveScene();
        if (gameMode == "level1") {
            SceneManager.LoadScene("levelSelect");
        } else {
		    SceneManager.LoadScene(gameMode);
        }
	}
}
