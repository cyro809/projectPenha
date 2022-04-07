using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    int lastClearedLevel;
    public GameObject[] levels;
    Color disabledTextColor = new Color(0.3f, 0.3f, 0.3f, 1.0f);
    Color enabledTextColor;
    void Start () {
        lastClearedLevel = PlayerPrefs.GetInt("lastClearedLevel", 0);
        // levels = GameObject.FindGameObjectsWithTag("levelButton");
        Debug.Log(lastClearedLevel);
        enabledTextColor = levels[0].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color;
        DisableAllLevels();
        EnableClearedLevels();
        Time.timeScale = 1;
    }


    // Update is called once per frame
    void Update()
    {

    }
    void DisableAllLevels() {
        for (int i=0; i< levels.Length; i++) {
            levels[i].GetComponent<Button>().interactable = false;
            levels[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = disabledTextColor;
        }
    }
    void EnableClearedLevels() {
        levels[0].GetComponent<Button>().interactable = true;
        levels[0].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = enabledTextColor;
        if(lastClearedLevel < levels.Length) {
            for (int i=0; i<=lastClearedLevel; i++) {
                levels[i].GetComponent<Button>().interactable = true;
                levels[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = enabledTextColor;
            }
        }
    }
}
