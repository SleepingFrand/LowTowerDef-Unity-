using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public int count = 0;
    public float speed = 1;
    public float speedRotate = 1;
    public GameObject[] roadPoint; 
    public float TimeSpawn = 1;
    public GameObject EnemyPrefab;
    float timespawn = 5;

    int count_now = 0;

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
