using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public AudioClip buttonClickSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SwitchScene1()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene("SelectScene");
    }

    public void SwitchScene2()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene("MainScenes");
    }

    private void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
