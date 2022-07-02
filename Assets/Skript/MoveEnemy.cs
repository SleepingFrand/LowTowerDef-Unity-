using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{

/*Характеристики врагов*/
    public GameObject[] points;
    public float speed = 1;
    public float speedRotate = 1;
    public int HP = 5;

/*Объект с игровым контроллером*/
    public GameObject PlayControl;

/*Функциональные переменные*/
    GameObject currentPoint;        // Текущая точка следования
    int point_toGo = 0;             // Номер текущей точки следования
    bool move_can = true;           // Не умер ли объект
    int HP_max;                     // Максимальное HP объекта

/*Объекты полоски HP*/
    GameObject HelfBar;
    GameObject HelfBar_Panel;

/*Характеристики повышения уровня*/
    public int HpPerLvl = 1;
    public int CountForLvl = 10;
    public int Lvl = 1;

    void Death()        // Смерть врага
    {
        move_can = false;
        this.GetComponent<Animator>().SetBool("Death", true);
        PlayControl.GetComponent<PlayControl>().Money += 10;
        Destroy(this.gameObject, 2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = HP + ((Lvl - 1) * HpPerLvl);
        HP_max = HP;
        PlayControl = GameObject.Find("Main Camera");
        currentPoint = points[point_toGo];
        HelfBar = this.transform.Find("HelfBar").gameObject;
        HelfBar_Panel = HelfBar.transform.Find("Panel").gameObject;
    }

    void Helfbar()          // Обновление полоски HP
    {
        HelfBar.transform.LookAt(PlayControl.transform);
        HelfBar_Panel.transform.localScale = new Vector3((float)HP / (float)HP_max >= 0? (float)HP / (float)HP_max:0, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (move_can)
        {
            if (Vector3.Distance(transform.position, currentPoint.transform.position) < 0.5f)
            {
                if (point_toGo + 1 < points.Length)
                {
                    point_toGo += 1;
                    currentPoint = points[point_toGo];
                }
                else
                {
                    PlayControl.GetComponent<PlayControl>().HP -= 1;
                    Destroy(this.gameObject);
                }
            }
            else
            {
                this.GetComponent<Animator>().SetBool("Walking", true);
                this.GetComponent<Animator>().speed = speed;

                Vector3 lookPos = currentPoint.transform.position - transform.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speedRotate * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, speed * Time.deltaTime);
            }

            if (HP <= 0)
                Death();

            Helfbar();
        }
    }
}
