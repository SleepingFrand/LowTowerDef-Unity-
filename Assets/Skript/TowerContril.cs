using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerContril : MonoBehaviour
{
    public int Damage = 1;
    public GameObject Bullet;
    public float SpeedBullet = 1;
    public float reload = 0.5f;
    float reload_now = 0;

    public int DamagePerLvl = 1;
    public int PrisePerLvl = 10;
    public int Lvl = 1;

    public int range = 10;

    GameObject[] enemy;
    public GameObject target;



    GameObject gun;

    void UpdateTarget() {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemy) { 
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance) { 
                shortestDistance = distanceToEnemy; nearestEnemy = enemy; 
            } 
        } 
        if (nearestEnemy != null && shortestDistance <= range && nearestEnemy.GetComponent<MoveEnemy>().HP >= 0) { 
            target = nearestEnemy; 
        } 
        else { 
            target = null; 
        } 
    }
    
    void Fire()
    {
        if (reload_now >= 0)
            reload_now -= Time.deltaTime;
        else
        {
            if (target)
            {
                GameObject bullet = Instantiate(Bullet, gun.transform.Find("SpawnBullet").position, gun.transform.rotation);
                bullet.GetComponent<BulletControl>().Target = target;
                bullet.GetComponent<BulletControl>().Damage = Damage + ((Lvl - 1) * DamagePerLvl);
                bullet.GetComponent<BulletControl>().Speed = SpeedBullet;
                reload_now = reload;
            }
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        gun = this.transform.Find("Turret").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (target && target.GetComponent<MoveEnemy>().HP > 0 && Vector3.Distance(transform.position, target.transform.position) <= range)
        {
            gun.transform.LookAt(target.transform);
            Fire();
        }
        else
        {
            UpdateTarget();
        }
    }
}
