using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlayControl : MonoBehaviour
{
    public int HP = 10;
    public int Money = 120;

    public GameObject GameO_HP;
    public GameObject GameO_Money;
    TMPro.TextMeshProUGUI TextUI_HP;
    TMPro.TextMeshProUGUI TextUI_Money;

    public string Text_HP;
    public string Text_Money;

    public Button Button_Build;
    public Button Button_Cell;

    public GameObject PlasePutNow;

    public GameObject TowerPrefab;

    public GameObject PutGameobject;
   

    public GameObject GameOPriseUp;
    public GameObject GameODamageTower;
    TMPro.TextMeshProUGUI TextUI_PriseUp;
    TMPro.TextMeshProUGUI TextUI_DamageTower;
    public string TextPriseTower;
    public string TextDamageTower;

    void OnMouseOver()
    {
        // Left Mouse Button
        if (Input.GetMouseButtonDown(0))
        {
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Plase")
                {
                    PlasePutNow = hit.transform.gameObject;
                    if (PutGameobject)
                    {
                        PutGameobject.transform.position = PlasePutNow.transform.position;
                    }
                }
            }
        }
    }

    void TextPut()
    {
        TextUI_HP.text = Text_HP + ' ' + HP.ToString();
        TextUI_Money.text = Text_Money + ' ' + Money.ToString();
        if (PlasePutNow)
        {
            if (PlasePutNow.GetComponent<PlaseControl>().Tower)
            {
                TextUI_PriseUp.text = TextPriseTower + ' ' + ((PlasePutNow.GetComponent<PlaseControl>().Tower.GetComponent<TowerContril>().Lvl *
                    PlasePutNow.GetComponent<PlaseControl>().Tower.GetComponent<TowerContril>().PrisePerLvl) + 100).ToString();
                TextUI_DamageTower.text = TextDamageTower + ' ' + (PlasePutNow.GetComponent<PlaseControl>().Tower.GetComponent<TowerContril>().Lvl *
                    PlasePutNow.GetComponent<PlaseControl>().Tower.GetComponent<TowerContril>().Damage).ToString();
            }
            else
            {
                TextUI_PriseUp.text = TextPriseTower + " 0";
                TextUI_DamageTower.text = TextDamageTower + " 0";
            }
        }
        else
        {
            TextUI_PriseUp.text = TextPriseTower + " 0";
            TextUI_DamageTower.text = TextDamageTower + " 0";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TextUI_HP = GameO_HP.GetComponent<TMPro.TextMeshProUGUI>();
        TextUI_Money = GameO_Money.GetComponent<TMPro.TextMeshProUGUI>();
        TextUI_PriseUp = GameOPriseUp.GetComponent<TMPro.TextMeshProUGUI>();
        TextUI_DamageTower = GameODamageTower.GetComponent<TMPro.TextMeshProUGUI>();
        TextPut();
    }

    // Update is called once per frame
    void Update()
    {
        OnMouseOver();
        TextPut();
    }

    public void Build()
    {
        if (PlasePutNow && Money >= 100)
        {
            if (!PlasePutNow.GetComponent<PlaseControl>().Tower)
            {
                GameObject tower = Instantiate(TowerPrefab, PlasePutNow.transform.position, PlasePutNow.transform.rotation);
                PlasePutNow.GetComponent<PlaseControl>().Tower = tower;
                Money -= 100;
            }
        }
    }
    public void Cell()
    {
        if (PlasePutNow )
        {
            if (PlasePutNow.GetComponent<PlaseControl>().Tower)
            {
                Destroy(PlasePutNow.GetComponent<PlaseControl>().Tower);
                PlasePutNow.GetComponent<PlaseControl>().Tower = null;
                Money += 80;
            }
        }
    }
    public void Up()
    {
        if (PlasePutNow) 
        {
            if (PlasePutNow.GetComponent<PlaseControl>().Tower)
            {
                if (Money >= (PlasePutNow.GetComponent<PlaseControl>().Tower.GetComponent<TowerContril>().Lvl *
            PlasePutNow.GetComponent<PlaseControl>().Tower.GetComponent<TowerContril>().PrisePerLvl) + 100)
                {
                    PlasePutNow.GetComponent<PlaseControl>().Tower.GetComponent<TowerContril>().Lvl += 1;
                    Money -= ((PlasePutNow.GetComponent<PlaseControl>().Tower.GetComponent<TowerContril>().Lvl - 1) *
                        PlasePutNow.GetComponent<PlaseControl>().Tower.GetComponent<TowerContril>().PrisePerLvl) + 100;
                }
            }
        }
    }
}
