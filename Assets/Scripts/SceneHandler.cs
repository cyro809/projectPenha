using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	public Slider musicSlider;
	public Slider soundEffectsSlider;
    public void StartGameAction(string sceneName) {
		SceneManager.LoadScene (sceneName);
		SetVolumes();
	}

	void SetVolumes() {
		PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
		PlayerPrefs.SetFloat("soudEffectsVolume", soundEffectsSlider.value);
	}

	void Start() {
	}
	
	public void LoadNewScene() {
		switch (this.gameObject.name)
		{
			case "EndlessModeButton":
			PlayerPrefs.SetString("gameMode", "mainGame");
				StartGameAction("instructions");
				break;
			case "AdventureModeButton":
				PlayerPrefs.SetString("gameMode", "level1");
				StartGameAction("instructions");
				break;
			case "CreditButton":
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
