using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR || UNITY_WEBGL
            GameObject exitButton = GameObject.Find("ExitButton");
            exitButton.SetActive(false);
        #endif
    }

    public void QuitGame() {
        Application.Quit();
    }
}
