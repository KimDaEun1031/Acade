using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        
        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }
    }
    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 2);
        int ranPoint = Random.Range(0, 9);
        GameObject enemy =Instantiate(enemyObjs[ranEnemy], 
                                spawnPoints[ranPoint].position, 
                                spawnPoints[ranPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;

        if(ranPoint == 7 || ranPoint == 8) //오른쪽 스폰
        {
            rigid.velocity = new Vector2(enemyLogic.speed*(-1), -1);
        }
        else if (ranPoint == 5 || ranPoint == 6) //왼쪽 스폰
        {
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else //앞 스폰
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed*(-1));
        }

    }

}
