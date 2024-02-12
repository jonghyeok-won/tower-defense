using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    private Image pauseBtnImage;
    private Image resumeBtnImage;
    private Image fastBtnImage;

    public Button pauseBtn;
    public Button resumeBtn;
    public Button fastBtn;

    public float originAlpha = 1.0f;
    public float targetAlpha = 0.4f;

    private void Start()
    {
        pauseBtnImage = pauseBtn.GetComponent<Image>();
        resumeBtnImage = resumeBtn.GetComponent<Image>();
        fastBtnImage = fastBtn.GetComponent<Image>();
    }


    public void PauseGame()
    {
        Time.timeScale = 0f;
        AdjustPauseBtnAlpha();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        AdjustResumeBtnAlpha();
    }

    public void DoubleSpeedGame()
    {
        Time.timeScale = 2f;
        AdjustFastAlpha();
    }

    private void AdjustPauseBtnAlpha()
    {
        pauseBtnImage.color = new Color(pauseBtnImage.color.r, pauseBtnImage.color.g, pauseBtnImage.color.b, targetAlpha);
        resumeBtnImage.color = new Color(pauseBtnImage.color.r, pauseBtnImage.color.g, pauseBtnImage.color.b, originAlpha);
        fastBtnImage.color = new Color(pauseBtnImage.color.r, pauseBtnImage.color.g, pauseBtnImage.color.b, originAlpha);
    }

    private void AdjustResumeBtnAlpha()
    {
        resumeBtnImage.color = new Color(pauseBtnImage.color.r, pauseBtnImage.color.g, pauseBtnImage.color.b, targetAlpha);
        pauseBtnImage.color = new Color(pauseBtnImage.color.r, pauseBtnImage.color.g, pauseBtnImage.color.b, originAlpha);
        fastBtnImage.color = new Color(pauseBtnImage.color.r, pauseBtnImage.color.g, pauseBtnImage.color.b, originAlpha);

    }

    private void AdjustFastAlpha()
    {
        fastBtnImage.color = new Color(pauseBtnImage.color.r, pauseBtnImage.color.g, pauseBtnImage.color.b, targetAlpha);
        resumeBtnImage.color = new Color(pauseBtnImage.color.r, pauseBtnImage.color.g, pauseBtnImage.color.b, originAlpha);
        pauseBtnImage.color = new Color(pauseBtnImage.color.r, pauseBtnImage.color.g, pauseBtnImage.color.b, originAlpha);
    }
}
