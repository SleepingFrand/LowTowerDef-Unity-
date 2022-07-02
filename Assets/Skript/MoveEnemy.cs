using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public GameObject[] points;
    public float speed = 1;
    public float speedRotate = 1;
    int point_toGo = 0;
    GameObject currentPoint;
    Quaternion direction;

    public GameObject PlayControl;

    public int HP = 5;

    int HP_max;

    bool move_can = true;

    GameObject HelfBar;
    GameObject HelfBar_Panel;

    public int HpPerLvl = 1;
    public int CountForLvl = 10;
    public int Lvl = 1;

    void Death()
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

    void Helfbar()
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
            {//-=Если есть еще точки на пути=-//
                if (point_toGo + 1 < points.Length)
                {//-=Определяем следующую точку=-//
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
            {//-=Движемся по пути=-//

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
