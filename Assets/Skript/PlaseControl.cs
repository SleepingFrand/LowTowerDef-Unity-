using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaseControl : MonoBehaviour
{

/*������ ����� �� ��������*/
    public GameObject Tower;

/*������������ ������ ��������*/
    public bool PutPlase = false;       // ��������� ������

    public Material BaseMaterial;       // �������� � ����������� ������
    public Material PutMaterial;        // �������� � ��������� ������

    public void SetMaterial()           // ������� ��������� ���������
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
