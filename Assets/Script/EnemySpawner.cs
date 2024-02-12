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
            // ���̺꿡 �����ϴ� ���� ������ ���� ������ �� ������ ���� �����ϵ��� �����ϰ� �� ������Ʈ ����
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
        //�� ü���� ��Ÿ���� slider UI ����
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);

        //silder UI ������Ʈ�� parent("Canvas"������Ʈ)�� �ڽ����� ����
        // UI�� ĵ������ �ڽĿ�����Ʈ�� �����Ǿ� �־�� ȭ�鿡 ����.
        sliderClone.transform.SetParent(canvasTransform);
        //���� �������� �ٲ� ũ�⸦ �ٽ� (1,1,1)�� ����
        sliderClone.transform.localScale = Vector3.one;

        //slider UI�� �Ѿƴٴ� ����� �������� ����
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        //slider UI�� �ڽ��� ü�� ������ ǥ���ϵ��� ����
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }
}
