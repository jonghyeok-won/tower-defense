using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public GameObject PopupWindow;

    private void Start()
    {
        PopupWindow.SetActive(false);
    }

    public void OpenPopup()
    {
        if (PopupWindow != null)
        {
            bool isActive = PopupWindow.activeSelf;

            PopupWindow.SetActive(!isActive);
        }
    }

    public void ClosePopup()
    {
        if (PopupWindow != null)
        {
            PopupWindow.SetActive(false);
        }
    }
}
