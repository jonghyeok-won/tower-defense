using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public AudioClip buttonClickSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Stage1()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene("GameScenes1");
    }

    public void Stage2()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene("GameScenes2");
    }

    public void Stage3()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene("GameScenes3");
    }

    private void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
