using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameType : MonoBehaviour
{
    // Start is called before the first frame update
    string gameMode;
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "mainGame") {
            gameMode = "survival";
        } else {
            gameMode = "adventure";
        }
    }

    public string GetGameMode() {
        return gameMode;
    }
}
