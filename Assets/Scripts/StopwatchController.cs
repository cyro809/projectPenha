using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopwatchController : MonoBehaviour
{
    bool started = false;
    public Text countText;
    // Start is called before the first frame update
    Stopwatch stopWatch;
    GameState gameState;
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
    void SetCountText() {
        TimeSpan ts = stopWatch.Elapsed;
        string elapsedTime = String.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds / 10);
		countText.text = "Time: " + elapsedTime;
	}
}
