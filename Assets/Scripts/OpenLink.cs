using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public void OpenFeedbackForm() {
        Application.OpenURL("https://forms.gle/9n3rnKD9pD6BNunX6");
        Canvas feedbackModalCanvas = GameObject.Find("FeedbackModal").GetComponent<Canvas>();
        feedbackModalCanvas.enabled = false;
    }
}
