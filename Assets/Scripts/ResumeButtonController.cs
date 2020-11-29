using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButtonController : MonoBehaviour
{
    public GameObject canvasGameObject;
    public void ResumeGame() {
        canvasGameObject.GetComponent<Canvas>().enabled = false;
		Time.timeScale = 1;
	}
}
