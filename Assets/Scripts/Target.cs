using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D cursorTarget;
    void Start()
    {
        Cursor.SetCursor(cursorTarget, new Vector2(16, 6), CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
