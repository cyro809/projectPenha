using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeOptions : MonoBehaviour
{
    public Slider musicSlider;
	public Slider soundEffectsSlider;
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.2f);
		soundEffectsSlider.value = PlayerPrefs.GetFloat("soudEffectsVolume", 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
