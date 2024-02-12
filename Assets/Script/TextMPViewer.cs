using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP;
    [SerializeField]
    private PlayerHP playerHP;
    [SerializeField]
    private TextMeshProUGUI textPlayerGold;
    [SerializeField]
    private PlayerGold playerGold;
    [SerializeField]
    private TextMeshProUGUI textWave;
    [SerializeField]
    private WaveSystem waveSystem;
    [SerializeField]
    private TextMeshProUGUI textEnemyCount;
    [SerializeField]
    private EnemySpawner enemySpawner;

    // Update is called once per frame
    private void Update()
    {
        textPlayerHP.text = playerHP.CurrentHP + "/" + playerHP.MaxHP;
        textPlayerGold.text = playerGold.CurrentGold.ToString();
        textWave.text = WaveSystem.CurrentWave + "/" + waveSystem.MaxWave;
        textEnemyCount.text = enemySpawner.CurrentEnemyCount + "/" + enemySpawner.MaxEnemyCount;
    }
}
