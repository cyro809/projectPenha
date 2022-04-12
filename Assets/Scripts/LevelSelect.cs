using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    public void LoadLevel() {
        string levelName = gameObject.name;
        if(levelName == "level1") {
            SceneManager.LoadScene("story");
        } else {
            SceneManager.LoadScene(levelName);
        }
    }

    public void LoadLevelSelectScreen() {
        SceneManager.LoadScene("levelSelect");
    }
}
