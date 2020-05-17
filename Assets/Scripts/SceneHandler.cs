using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	public Slider musicSlider;
	public Slider soundEffectsSlider;
    public void StartGameAction() {
		SceneManager.LoadScene ("mainGame");
		PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
		PlayerPrefs.SetFloat("soudEffectsVolume", soundEffectsSlider.value);
	}
	
	public void LoadNewScene() {
		switch (this.gameObject.name)
		{
			case "StartButton":
				StartGameAction();
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
