using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
	public Slider musicSlider;
    public void StartGameAction() {
		SceneManager.LoadScene ("mainGame");
		PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
	}
}
