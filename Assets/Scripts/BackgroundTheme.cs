using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTheme : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip gameOverClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameOverClip = Resources.Load<AudioClip>("Music/game-over-sound");
    }

    public void stopAudioSouce() {
        audioSource.Stop();
    }

    public void ToggleToGameOverSound() {
        Debug.Log(gameOverClip);
        audioSource.clip = gameOverClip;
        audioSource.loop = false;
        audioSource.Play();
    }

}
