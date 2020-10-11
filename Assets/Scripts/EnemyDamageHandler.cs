using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    public int enemyHealth = 100;

    bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        isAlive = true;
    }

    public void TakeDamage(int hitDamage)
    {
        enemyHealth -= hitDamage;
    }

    private void OnParticleCollision(GameObject other)
    {
        
        if (isAlive && other.CompareTag(TAGS.Bullet))
        {
            int damage = other.transform.root.GetComponent<TowerController>().bulletDamage;
            OnBulletHit(damage);
        }

    }

    private void OnBulletHit(int hitDamage)
    {
        TakeDamage(hitDamage);
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            isAlive = false;
        }
    }
}
