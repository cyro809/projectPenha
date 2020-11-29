using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	public void RestartGame() {
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
		Time.timeScale = 1;
	}
}
