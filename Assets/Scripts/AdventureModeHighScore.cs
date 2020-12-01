using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdventureModeHighScore : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject scoreGameObject;
    public GameObject gameStateObject;
    double highScore;
    StopwatchController score;
    GameState gameState;
    public TextMeshProUGUI highScoreText;
    string sceneName;
    string sceneHighscorePrefName;
    void Start()
    {
        score = scoreGameObject.GetComponent<StopwatchController>();
        sceneName = SceneManager.GetActiveScene().name;
        sceneHighscorePrefName = sceneName + "highScore";
        highScore = double.Parse(PlayerPrefs.GetString(sceneHighscorePrefName, score.GetMillisecondsTime().ToString()));
        gameState = gameStateObject.GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState.gameWin) {
            if(score.GetMillisecondsTime() < highScore || highScore == 0) {
                PlayerPrefs.SetString(sceneHighscorePrefName, score.GetMillisecondsTime().ToString());
            }
        }
        updateText();
    }

    void updateText() {
        highScoreText.text = "High Score: " + score.FormatMillisecondsTime(highScore);
    }
}
