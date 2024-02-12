using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /*[SerializeField]
    private GameObject enemyPrefab;*/
    [SerializeField]
    private GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;
    /*[SerializeField]
    private float spawnTime;*/
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private PlayerHP playerHP;
    [SerializeField]
    private PlayerGold playerGold;
    private Wave currentWave;
    private int currentEnemyCount;
    private List<Enemy> enemyList;

    public List<Enemy> EnemyList => enemyList;
    public int CurrentEnemyCount => currentEnemyCount;
    public int MaxEnemyCount => currentWave.maxEnemyCount;

    private void Awake()
    {
        enemyList = new List<Enemy>();

        //StartCoroutine("SpawnEnemy");
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave;
        currentEnemyCount = currentWave.maxEnemyCount;
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        int spawnEnemyCount = 0;
        int currentElementIndex = 0;

        while (spawnEnemyCount < currentWave.maxEnemyCount)
        {
            if (currentElementIndex < currentWave.elements.Length)
            {
                Wave.WaveElement element = currentWave.elements[currentElementIndex];

                for (int i = 0; i < element.count; i++)
                {
                    GameObject clone = Instantiate(element.enemyPrefab);
                    Enemy enemy = clone.GetComponent<Enemy>();

                    enemy.Setup(this, wayPoints);
                    enemyList.Add(enemy);

                    SpawnEnemyHPSlider(clone);

                    spawnEnemyCount++;

                    yield return new WaitForSeconds(currentWave.spawnTime);
                }

                currentElementIndex++;
            }
            else
            {
                yield break;
            }
        }
    }


    /*private IEnumerator SpawnEnemy()
    {
        int spawnEnemyCount = 0;

        while (spawnEnemyCount < currentWave.maxEnemyCount)
        {
            //GameObject clone = Instantiate(enemyPrefab);
            // 웨이브에 등장하는 적의 종류가 여러 종류일 때 임의의 적이 등장하도록 설정하고 적 오브젝트 생성
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);

            SpawnEnemyHPSlider(clone);

            spawnEnemyCount++;

            //yield return new WaitForSeconds(spawnTime);
            yield return new WaitForSeconds(currentWave.spawnTime);
        }
    }*/

    public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, int gold)
    {
        if(type == EnemyDestroyType.Arrive)
        {
            playerHP.TakeDamage(1);
        }

        else if(type == EnemyDestroyType.Kill)
        {
            playerGold.CurrentGold += gold;
        }

        currentEnemyCount--;

        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        //적 체력을 나타내는 slider UI 생성
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);

        //silder UI 오브젝트를 parent("Canvas"오브잭트)의 자식으로 설정
        // UI는 캔버스의 자식오브젝트로 설정되어 있어야 화면에 보임.
        sliderClone.transform.SetParent(canvasTransform);
        //계층 설정으로 바뀐 크기를 다시 (1,1,1)로 설정
        sliderClone.transform.localScale = Vector3.one;

        //slider UI가 쫓아다닐 대상을 본인으로 설정
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        //slider UI에 자신의 체력 정보를 표시하도록 설정
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }
}
