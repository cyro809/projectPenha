using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Text countDownText;
    public bool finishCount;
    void Start()
    {
        countDownText = gameObject.GetComponent<Text> ();
        finishCount = false;
        StartCoroutine(CountdownEnum(3));
    }
   
    IEnumerator CountdownEnum(int seconds)
    {
        int count = seconds;
       
        while (count > 0) {
           countDownText.text = "" + count.ToString ();
            // display something...
            yield return new WaitForSeconds(1);
            count --;
        }
       
        // count down is finished...
        StartGame();
    }
 
    void StartGame()
    {
        countDownText.text = "GO!";
        finishCount = true;
        // do something...
    }
}
