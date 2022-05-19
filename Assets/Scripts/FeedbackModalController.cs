using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackModalController : MonoBehaviour
{
    public void OpenFeedbackModal() {
        Canvas feedbackModalCanvas = GameObject.Find("FeedbackModal").GetComponent<Canvas>();
        feedbackModalCanvas.enabled = true;
    }

    public void CloseFeedbackModal() {
        Canvas feedbackModalCanvas = GameObject.Find("FeedbackModal").GetComponent<Canvas>();
        feedbackModalCanvas.enabled = false;
    }
}
