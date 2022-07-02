using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{

/*Характеристики врагов*/
    public GameObject EnemyPrefab;
    public float speed = 1;
    public float speedRotate = 1;

/*Характеристики врат*/
    public GameObject[] roadPoint;          // Точки пути
    public int count = 0;                   // Кол-во врагов
    public float TimeSpawn = 1;             // Время появления

/*Характеристики врат*/
    float timespawn = 5;                    // Задержка первого появления
    int count_now = 0;                      // Подсчет появивщихся врагов

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(count > 0)
            if(timespawn > 0)
                timespawn -= Time.deltaTime;
            else{
                SpawnEnemy();
                timespawn = TimeSpawn;
                count--;
            }

    }

    void SpawnEnemy(){
        count_now++;
        GameObject enemy = Instantiate(EnemyPrefab, this.transform.Find("SpawnPoint").position, this.transform.Find("SpawnPoint").rotation);
        enemy.GetComponent<MoveEnemy>().points = roadPoint;
        enemy.GetComponent<MoveEnemy>().speed = speed;
        enemy.GetComponent<MoveEnemy>().speedRotate = speedRotate;
        enemy.GetComponent<MoveEnemy>().Lvl = count_now / 10 + 1;
    }
}
