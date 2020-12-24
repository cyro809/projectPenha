using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    int lastClearedLevel;
    public GameObject[] levels;
    void Start () {
        lastClearedLevel = PlayerPrefs.GetInt("lastClearedLevel", 0);
        // levels = GameObject.FindGameObjectsWithTag("levelButton");
        DisableAllLevels();
        EnableClearedLevels();
    }


    // Update is called once per frame
    void Update()
    {

    }
    void DisableAllLevels() {
        for (int i=0; i< levels.Length; i++) {
            levels[i].GetComponent<Button>().interactable = false;
        }
    }
    void EnableClearedLevels() {
        levels[0].GetComponent<Button>().interactable = true;
        for (int i=0; i<=lastClearedLevel; i++) {
            levels[i].GetComponent<Button>().interactable = true;
        }

    }
}
