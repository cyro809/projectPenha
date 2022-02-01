using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderBoardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetText());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GetText() {
        UnityWebRequest www = UnityWebRequest.Get("https://project-penha-api.herokuapp.com/scores/survival");
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            string jsonString = www.downloadHandler.text;
            LeaderBoardScore[] leaders = JsonHelper.FromJson<LeaderBoardScore>(jsonString);
            // Show results as text
            Debug.Log(jsonString);
            Debug.Log(leaders[0].score);
            Debug.Log(leaders[0].name);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
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