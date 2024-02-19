using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : AbstractSingleton<MonoBehaviour>
{
    public AudioSource AudioSource;
    public AudioClip[] AudioClips;
    
    protected override void OnAwake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "TitleScene":
                PlayAudio(0);
                break;
            case "DeckSelectScene":
                PlayAudio(1);
                break;
            case "InGameScene":
                PlayAudio(2);
                break;
        }
        AudioSource = FindObjectOfType<AudioSource>();
    }

    public void PlayAudio(int audioIndex)
    {
        AudioSource.clip = AudioClips[audioIndex];
        AudioSource.Play();
    }

    public void StopAudio()
    {
        AudioSource.Stop();
    }
    
    
}
