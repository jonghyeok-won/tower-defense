using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    [SerializeField]
    private int currentCold = 100;

    public int CurrentGold
    {
        set => currentCold = Mathf.Max(0,value);
        get => currentCold;
    }
}
