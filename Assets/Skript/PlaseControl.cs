using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaseControl : MonoBehaviour
{

/*Объект башни на площадке*/
    public GameObject Tower;

/*Вызуализация выбора площадки*/
    public bool PutPlase = false;       // Состояние выбора

    public Material BaseMaterial;       // Материал в стандартном режиме
    public Material PutMaterial;        // Материал в выбранном режиме

    public void SetMaterial()           // Функция установки материала
    {
        if (PutPlase)
            this.gameObject.GetComponent<Renderer>().material = PutMaterial;
        else
            this.gameObject.GetComponent<Renderer>().material = BaseMaterial;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Renderer>().material = BaseMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
