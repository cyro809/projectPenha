using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider musicSlider;
    public Slider soundEffectsSlider;
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.2f);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);

        soundEffectsSlider.value = PlayerPrefs.GetFloat("soudEffectsVolume", 0.4f);
        PlayerPrefs.SetFloat("soudEffectsVolume", soundEffectsSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("soudEffectsVolume", soundEffectsSlider.value);
    }
}
