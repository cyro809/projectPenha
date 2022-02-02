using System;
using System.Text;
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
    void Start()
    {
        // PostAdventureScore("bar", "level1", 950.20f);
        GetAdventureScore("1");
    }

    public void GetSurvivalScores() {
        StartCoroutine(GetScore(API_HOST + "survival", SURVIVAL_MODE));
    }

    public void GetAdventureScore(string levelNumber) {
        StartCoroutine(GetScore(API_HOST + "/level/" + levelNumber, ADVENTURE_MODE));
    }

    IEnumerator GetScore(string url, int mode) {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            string jsonString = www.downloadHandler.text;

            if(mode == SURVIVAL_MODE) {
                SurvivalLeaderBoardScore[] leaders = JsonHelper.FromJson<SurvivalLeaderBoardScore>(jsonString);
                FormatSurvivalLeaderboard(leaders);
            }
            else {
                AdventureLeaderBoardScore[] leaders = JsonHelper.FromJson<AdventureLeaderBoardScore>(jsonString);
                FormatAdventureLeaderboard(leaders);
            }

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }

    // Code reference: https://stackoverflow.com/a/45476691
    IEnumerator PostScore(string url, string data) {
        UnityWebRequest request = UnityWebRequest.Post(url, data);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(data);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.method = "POST";
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.Log("Erro: " + request.error);
        }
        else
        {
            Debug.Log("All OK");
            Debug.Log("Status Code: " + request.responseCode);
        }
    }

    void FormatSurvivalLeaderboard(SurvivalLeaderBoardScore[] leaders) {
        Text leaderBoardText = gameObject.GetComponent<Text>();
        leaderBoardText.text = "Pos.\t\t\tName\t\t\tScore\n";
        for (int i=0; i <leaders.Length; i++) {
            int position = i+1;
            leaderBoardText.text += position.ToString() + "  \t\t\t" + leaders[i].name + "  \t\t\t" + leaders[i].score;
            leaderBoardText.text += "\n";
        }
    }

    void FormatAdventureLeaderboard(AdventureLeaderBoardScore[] leaders) {
        Text leaderBoardText = gameObject.GetComponent<Text>();
        leaderBoardText.text = "Pos.\t\t\tName\t\t\tScore\n";
        for (int i=0; i <leaders.Length; i++) {
            int position = i+1;
            leaderBoardText.text += position.ToString() + "  \t\t\t" + leaders[i].name + "  \t\t\t" + leaders[i].score;
            leaderBoardText.text += "\n";
        }
    }

    void PostSurvivalScore(string name, int score) {
        string data = "{\"name\": \""+ name + "\", \"score\": "+ score.ToString() + ", \"table\": \"survival_scores\"}";
        Debug.Log(data);
        StartCoroutine(PostScore(API_HOST + "add", data));
    }

    void PostAdventureScore(string name, string level, float score) {
        string data = "{\"name\": \""+ name + "\", \"score\": "+ score.ToString() + ", \"table\": \""+ level + "_scores\"}";
        Debug.Log(data);
        StartCoroutine(PostScore(API_HOST + "add", data));
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