using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{

/*Характеристики пули*/
    public GameObject Target;
    public float Speed;
    public int Damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
            Destroy(this.gameObject);
        if (Vector3.Distance(this.transform.position, Target.transform.position) < 0.5f)
        {
            Target.GetComponent<MoveEnemy>().HP -= Damage;
            Destroy(this.gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
            transform.LookAt(Target.transform);
        }
        
    }
}
