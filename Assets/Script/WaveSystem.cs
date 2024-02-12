using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private EnemySpawner enemySpawner;
    private static int currentWaveIndex = -1;

    [SerializeField]
    private SystemTextViewer systemTextViewer;

    public static int CurrentWave => currentWaveIndex + 1;
    public int MaxWave => waves.Length;

    [SerializeField]
    private float enemyHPIncrease = 2f;

    public void StartWave()
    {
        if(enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length-1)
        {
            currentWaveIndex++;
            enemySpawner.StartWave(waves[currentWaveIndex]);

            if(CurrentWave %5  == 1 && CurrentWave > 2)
            {
                IncreaseEnemyHP();
            }
        }
        else
        {
            systemTextViewer.PrintText(SystemType.EnemyRemain);
        }
    }

    private void IncreaseEnemyHP()
    {
        foreach(Wave.WaveElement element in waves[currentWaveIndex].elements)
        {
            EnemyHP enemyHP = element.enemyPrefab.GetComponent<EnemyHP>();
            if(enemyHP != null)
            {
                enemyHP.IncreaseMaxHP(enemyHPIncrease);
            }
        }
        systemTextViewer.PrintText(SystemType.EnemyHP);
    }
    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (currentWaveIndex >= waves.Length - 1 && enemySpawner.EnemyList.Count == 0)
        {
            SceneManager.LoadScene("ClearScenes");
        }
    }
}

[System.Serializable]
public struct Wave
{
    public WaveElement[] elements;
    public float spawnTime;
    public int maxEnemyCount;

    [System.Serializable]
    public struct WaveElement
    {
        public GameObject enemyPrefab;
        public int count;
    }
}
