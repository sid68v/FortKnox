using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    [SerializeField] Transform turret;

    [SerializeField] GameObject bulletGeneratorGO;

    public int bulletDamage = 10;
    public float attackRange = 30;



    GameObject[] enemies;
    GameObject nearestEnemy;

    // Start is called before the first frame update
    void Start()
    {


        bulletGeneratorGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        CheckForEnemies();
    }

    private void CheckForEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag(TAGS.Enemy);
        //if enemies exist, check for nearest enemy
        if (enemies.Length > 0 && IsEnemyNear())
        {
            AttackEnemy(true);
        }
        else
        {
            AttackEnemy(false);
        }
    }



    private void AttackEnemy(bool isAttacking)
    {
        if (isAttacking)
        {
            turret.LookAt(nearestEnemy.transform);
        }
        bulletGeneratorGO.SetActive(isAttacking);
    }

    private bool IsEnemyNear()
    {
        List<float> distances = new List<float>();

        foreach (GameObject enemy in enemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            distances.Add(enemyDistance);
        }
        float minDistance = distances.Min();

        if (minDistance > attackRange)
        {
            return false;
        }

        int indexofnearestEnemy = distances.IndexOf(minDistance);
        nearestEnemy = enemies[indexofnearestEnemy];

        return true;
    }
}
