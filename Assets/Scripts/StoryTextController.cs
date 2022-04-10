using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTextController : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI text;
    float speed = 0.001f;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        text.color = new Color(1.0f, 1.0f, 1.0f, speed);
        speed += 0.005f;
    }
}
