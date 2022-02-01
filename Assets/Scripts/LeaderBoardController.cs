using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LeaderBoardController : MonoBehaviour
{
    // Start is called before the first frame update
    const string API_HOST = "https://project-penha-api.herokuapp.com/scores/";
    const int SURVIVAL_MODE = 0;
    const int ADVENTURE_MODE = 1;
    int mode = 0;
    public Text leaderBoardText;
    void Start()
    {
        leaderBoardText = gameObject.GetComponent<Text>();
        GetSurvivalScores();
    }

    public void GetSurvivalScores() {
        mode = SURVIVAL_MODE;
        StartCoroutine(GetScore(API_HOST + "survival"));
    }

    public void GetAdventureScore(string levelNumber) {
        mode = ADVENTURE_MODE;
        StartCoroutine(GetScore(API_HOST + "/level/" + levelNumber));
    }

    IEnumerator GetScore(string url) {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            string jsonString = www.downloadHandler.text;
            SurvivalLeaderBoardScore[] leaders = JsonHelper.FromJson<SurvivalLeaderBoardScore>(jsonString);
            // Show results as text
            FormatSurvivalLeaderboard(leaders);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }

    void FormatSurvivalLeaderboard(SurvivalLeaderBoardScore[] leaders) {
        leaderBoardText.text = "Pos.\t\t\tName\t\t\tScore\n";
        for (int i=0; i <leaders.Length; i++) {
            int position = i+1;
            leaderBoardText.text += position.ToString() + "  \t\t\t" + leaders[i].name + "  \t\t\t" + leaders[i].score;
            leaderBoardText.text += "\n";
        }
    }
}



/*
    Code reference: https://stackoverflow.com/a/36244111
*/
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.scores;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.scores = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.scores = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] scores;
    }
}