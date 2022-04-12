using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryButtonController : MonoBehaviour
{
    public void LoadFirstLevel() {
        SceneManager.LoadScene("level1");
    }
}
