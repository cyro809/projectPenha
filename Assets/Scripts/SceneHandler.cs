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
		PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
		PlayerPrefs.SetFloat("soudEffectsVolume", soundEffectsSlider.value);
	}
	
	public void LoadNewScene() {
		switch (this.gameObject.name)
		{
			case "EndlessModeButton":
				StartGameAction("mainGame");
				break;
			case "AdventureModeButton":
				StartGameAction("level1");
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
