using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void StartGameAction(string sceneName) {
		SceneManager.LoadScene (sceneName);
	}

	public void LoadNewScene() {
		switch (this.gameObject.name)
		{
			case "GameStartButton":
				SceneManager.LoadScene("gameModes");
				break;
			case "SurvivalModeButton":
			PlayerPrefs.SetString("gameMode", "mainGame");
				StartGameAction("instructions");
				break;
			case "AdventureModeButton":
				PlayerPrefs.SetString("gameMode", "level1");
				StartGameAction("instructions");
				break;
			case "CreditsButton":
				SceneManager.LoadScene("credits");
				break;
			case "TitleButton":
				SceneManager.LoadScene("titleScreen");
				break;
			default:
				break;
		}
	}
}
