using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D cursorTarget;
    public GameObject gameState;
    void Start()
    {
        Cursor.SetCursor(cursorTarget, new Vector2(16, 6), CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStopped()) {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    bool isGameStopped() {
        GameState gs = gameState.GetComponent<GameState>();
        return (gs.gameOver || gs.gameWin || gs.gamePaused);
    }
}
