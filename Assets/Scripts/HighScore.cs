using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject scoreGameObject;
    public GameObject gameStateObject;
    int highScore;
    Score score;
    GameState gameState;
    public TextMeshProUGUI highScoreText;
    void Start()
    {
        score = scoreGameObject.GetComponent<Score>();
        highScore = PlayerPrefs.GetInt("highScore", score.getCounter());
        gameState = gameStateObject.GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState.gameOver) {
            if(score.getCounter() > highScore) {
                PlayerPrefs.SetInt("highScore", score.getCounter());
            }
        }
        updateText();
    }

    void updateText() {
        highScoreText.text = "High Score: " + highScore;
    }
}
