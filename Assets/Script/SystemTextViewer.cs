using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum SystemType { Money = 0, Build, EnemyHP, EnemyRemain }

public class SystemTextViewer : MonoBehaviour
{
    private TextMeshProUGUI textSystem;
    private TMPAlpha tmpAlpha;

    private void Awake()
    {
        textSystem = GetComponent<TextMeshProUGUI>();
        tmpAlpha = GetComponent<TMPAlpha>();
    }

    public void PrintText(SystemType type)
    {
        switch (type)
        {
            case SystemType.Money:
                textSystem.text = "System : Not enough money";
                break;
            case SystemType.Build:
                textSystem.text = "System : Invalid build tower";
                break;
            case SystemType.EnemyHP:
                textSystem.text = "System : Enemies are getting stronger";
                break;
            case SystemType.EnemyRemain:
                textSystem.text = "System : Enemis are remain. You can`t start next wave.";
                break;
        }
        tmpAlpha.FadeOut();
    }
}
