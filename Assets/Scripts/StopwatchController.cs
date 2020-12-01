using TMPro;
using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopwatchController : MonoBehaviour
{
    bool started = false;
    public TextMeshProUGUI countText;
    // Start is called before the first frame update
    Stopwatch stopWatch;
    GameState gameState;
    double millisecondsTime;
    void Start()
    {
        gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();
        stopWatch = new Stopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        if(!started && gameState.gameStart) {
            stopWatch.Start();
            started = true;
        }
        if(started && gameState.gameOver) {
            stopWatch.Stop();
            started = false;
        }
        SetCountText();
    }

    public double GetMillisecondsTime() {
        return millisecondsTime;
    }

    void SetCountText() {
        TimeSpan ts = stopWatch.Elapsed;
        millisecondsTime = ts.TotalMilliseconds;
        string elapsedTime = FormatMillisecondsTime(millisecondsTime);
		countText.text = "Time: " + elapsedTime;
	}

    public string FormatMillisecondsTime(double milliseconds) {
        return String.Format("{0:00.00}", milliseconds / 1000);
    }
}
