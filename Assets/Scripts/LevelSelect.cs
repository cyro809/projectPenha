using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    public void LoadLevel() {
        string levelName = gameObject.name;
        SceneManager.LoadScene(levelName);
    }

}
