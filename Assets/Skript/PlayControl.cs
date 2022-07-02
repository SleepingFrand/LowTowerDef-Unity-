using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class PlayControl : MonoBehaviour
{

/*�������� ����������*/

    public int HP = 10;                 //����� ������
    public int Money = 120;             //���-�� ��������� ������

/*��������� ��������� �������� UI*/

    //����� ������
    public GameObject GameO_HP;         //������
    TMPro.TextMeshProUGUI TextUI_HP;    //���������
    public string Text_HP;              //������ ������ � ������

    //������
    public GameObject GameO_Money;
    TMPro.TextMeshProUGUI TextUI_Money;
    public string Text_Money;

    //��������� ��������� ��������� �����
    public GameObject GameOPriseUp;
    TMPro.TextMeshProUGUI TextUI_PriseUp;
    public string TextPriseTower;

    //���� ��������� �����
    public GameObject GameODamageTower;
    TMPro.TextMeshProUGUI TextUI_DamageTower;
    public string TextDamageTower;

/*�������������� �������*/

    public GameObject TowerPrefab;          //������ �����

    public GameObject PlasePut;             //��������� ���������

    TowerContril TowerPut;                  //��������� ������� ����������� �����

    public GameObject Menu;                 //������ ����
    public GameObject GameOver;             //������ ������� � ���������;


    void OnMouseOver()          // ����� ���������
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Plase")
                {
                    if (PlasePut)
                    {
                        // ����� ��������� ����� ���������� ����� ��������. �� ����� ��� ����� (��������/����������) ��������� ���������� �����,
                        // � ������ ����� ������� ��������
                        PlasePut.GetComponent<PlaseControl>().PutPlase = false;
                        PlasePut.GetComponent<PlaseControl>().SetMaterial();
                    }
                    PlasePut = hit.transform.gameObject;
                    PlasePut.GetComponent<PlaseControl>().PutPlase = true;
                    PlasePut.GetComponent<PlaseControl>().SetMaterial();
                    if (PlasePut.GetComponent<PlaseControl>().Tower)
                        TowerPut = PlasePut.GetComponent<PlaseControl>().Tower.GetComponent<TowerContril>();
                    else
                        TowerPut = null;
                }
            }
        }
    }

    void TextPut()          // ���������� ������
    {
        TextUI_HP.text = Text_HP + ' ' + HP.ToString();
        TextUI_Money.text = Text_Money + ' ' + Money.ToString();
        if (PlasePut)
        {
            if (TowerPut)
            {
                TextUI_PriseUp.text = TextPriseTower + ' ' + ((TowerPut.Lvl * TowerPut.PrisePerLvl) + 100).ToString();
                TextUI_DamageTower.text = TextDamageTower + ' ' + (TowerPut.Lvl *
                    TowerPut.Damage).ToString();
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
    }

    void Pause()
    {
        Time.timeScale = 0f;
        Menu.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
        Menu.SetActive(false);
    }

    void Chaeck_HP()
    {
        if(HP <= 0)
        {
            Pause();
            GameOver.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        OnMouseOver();
        TextPut();
        Chaeck_HP();
        if (Input.GetKey("escape"))
            Pause();
    }

    public void Build()         // ���������� ������ �������������
    {
        if (PlasePut && Money >= 100)
        {
            if (!TowerPut)
            {
                GameObject tower = Instantiate(TowerPrefab, PlasePut.transform.position, PlasePut.transform.rotation);
                PlasePut.GetComponent<PlaseControl>().Tower = tower;
                TowerPut = PlasePut.GetComponent<PlaseControl>().Tower.GetComponent<TowerContril>();
                Money -= 100;
            }
        }
    }
    public void Cell()          // ���������� ������ �������
    {
        if (PlasePut )
        {
            if (TowerPut)
            {
                Destroy(PlasePut.GetComponent<PlaseControl>().Tower);
                PlasePut.GetComponent<PlaseControl>().Tower = null;
                Money += 80;
            }
        }
    }
    public void Up()            // ���������� ������ ���������
    {
        if (PlasePut) 
        {
            if (TowerPut)
            {
                if (Money >= (TowerPut.Lvl * TowerPut.PrisePerLvl) + 100)
                {
                    Money -= (TowerPut.Lvl * TowerPut.PrisePerLvl) + 100;
                    TowerPut.Lvl += 1;
                }
            }
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
