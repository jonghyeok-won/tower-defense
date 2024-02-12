using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbility : MonoBehaviour
{
    public bool healAbility = true;
    public bool dashAbility = true;
    public bool invisibleAbility = true;
    public bool breakAbility = true;

    // �� �ɷ¿� ���� ������
    public float dashSpeedIncrease = 3.0f;
    public float dashTime = 5.0f;
    public float dashDuration = 5f;
    public float invincibleTime = 5.0f;

    private Enemy enemy;
    private EnemyHP enemyHP;
    private Movement2D movement;

    // ȸ�� �ɷ� ����
    public Sprite healEffectSprite; // ü�� ȸ�� ȿ�� ��������Ʈ
    private GameObject healEffect; // ü�� ȸ�� ȿ�� ���� ������Ʈ
    private SpriteRenderer spriteRenderer; // ü�� ȸ�� ȿ���� SpriteRenderer
    public float healTime = 5f;
    public float healPercent = 0.05f; // ȸ���� (�ִ� ü���� ����)

    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyHP = GetComponent<EnemyHP>();
        movement = GetComponent<Movement2D>();

        // ü�� ȸ�� ȿ�� ���� ������Ʈ�� SpriteRenderer ����
        healEffect = new GameObject("HealEffect");
        spriteRenderer = healEffect.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = healEffectSprite;
        healEffect.transform.SetParent(this.transform);
        healEffect.transform.localPosition = Vector3.zero;
        healEffect.transform.localScale = new Vector3(0.33f, 0.33f, 1f);
        healEffect.SetActive(false);
        
        StartCoroutine(HealOverTime());
        StartCoroutine(Dash());
        StartCoroutine(Invisible());
        StartCoroutine(Break());
    }

    void Update()
    {
        if (!healAbility && WaveSystem.CurrentWave >= 4)
            healAbility = true;

        if (!dashAbility && WaveSystem.CurrentWave >= 9)
        {
            dashAbility = true;
            StartCoroutine(Dash());
        }

        if (!invisibleAbility && WaveSystem.CurrentWave >= 14)
        {
            invisibleAbility = true;
            StartCoroutine(Invisible());
        }

        if (!breakAbility && WaveSystem.CurrentWave >= 18)
        {
            breakAbility = true;
            StartCoroutine(Break());
        }
    }

    IEnumerator HealOverTime()
    {
        while (!enemyHP.isDie)
        {
            yield return new WaitForSeconds(healTime);
            if (healAbility && enemyHP.CurrentHP < enemyHP.MaxHP)
            {
                float healAmount = enemyHP.MaxHP * healPercent;
                enemyHP.Heal(healAmount);
                StartCoroutine(FadeHealEffect());
            }
        }
    }

    IEnumerator FadeHealEffect()
    {
        healEffect.SetActive(true);
        Color color = spriteRenderer.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime;
            spriteRenderer.color = color;
            yield return null;
        }
        color.a = 1f;
        spriteRenderer.color = color;
        healEffect.SetActive(false);
    }

    IEnumerator Dash()
    {
        while (!enemyHP.isDie)
        {
            yield return new WaitForSeconds(dashTime);
            if (dashAbility)
            {
                float originalSpeed = movement.MoveSpeed;
                movement.MoveSpeed *= dashSpeedIncrease;
                yield return new WaitForSeconds(dashDuration);
                movement.MoveSpeed = originalSpeed;
            }
        }
    }

    IEnumerator Invisible()
    {
        while (!enemyHP.isDie)
        {
            yield return new WaitForSeconds(invincibleTime);    
            if (invisibleAbility)
            {
                enemyHP.IsInvincible = true;
                yield return new WaitForSeconds(invincibleTime);
                enemyHP.IsInvincible = false;
            }
        }
    }

    IEnumerator Break()
    {
        while (!enemyHP.isDie)
        {
            yield return new WaitForSeconds(10.0f);
            if (breakAbility)
            {
                GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
                if (towers.Length > 0)
                {
                    GameObject randomTower = towers[Random.Range(0, towers.Length)];
                    Destroy(randomTower);
                }
            }
        }
    }
}
