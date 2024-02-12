using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projecttile : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;
    private float damage;
    private Vector3 direction;

    public void Setup(Transform target, float damage)
    {       
        movement2D = GetComponent<Movement2D>();
        this.target = target;
        this.damage = damage;        
    }

    private void Update()
    {
        if(target != null)
        {
            direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Projectile has collided with: " + collision.gameObject.name);
        if (!collision.CompareTag("Enemy")) return;
        if (collision.transform != target) return;

        //collision.GetComponent<Enemy>().OnDie();
        collision.GetComponent<EnemyHP>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
