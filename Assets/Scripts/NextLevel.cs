using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextLevel : MonoBehaviour
{
   public void GoToNextLevel() {
		 Scene scene = SceneManager.GetActiveScene();
         
         int currentLevelNumber = Convert.ToInt32(Regex.Split(scene.name, "level")[1]);
         currentLevelNumber++;
		 SceneManager.LoadScene("level" + currentLevelNumber);
	}
}
