using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider musicSlider;
    void Start()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }
}
