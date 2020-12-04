using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI countDownText;
    public bool finishCount;
    void Start()
    {
        countDownText = gameObject.GetComponent<TextMeshProUGUI> ();
        finishCount = false;
        StartCoroutine(CountdownEnum(1));
    }

    IEnumerator CountdownEnum(int seconds)
    {
        int count = seconds;

        while (count > 0) {
           countDownText.text = "Ready";
            // display something...
            yield return new WaitForSeconds(1);
            count --;
        }

        // count down is finished...
        countDownText.text = "GO!";
        Invoke("StartGame", 1);
    }

    void StartGame()
    {

        finishCount = true;
        // do something...
    }
}
