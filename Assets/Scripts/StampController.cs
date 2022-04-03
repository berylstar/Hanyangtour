using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampController : MonoBehaviour
{
    public GameObject canvasMain;
    public GameObject canvasStamp;

    public int stampNum;

    public GameObject check01;  //�ϳ���
    public GameObject check02;  //����
    public GameObject check03;  //���۷���Ȧ
    public GameObject check04;  //��Ʋ��
    public GameObject check05;  //�ư��
    public GameObject check06;  //����
    public GameObject check07;  //������

    void Start()
    {

    }   

    void Update()
    {
        Prediction pred = canvasMain.GetComponent<Prediction>();
        stampNum = pred.classNum;

        StampOnOff();
    }

    public void ButtonToStamp()
    {
        canvasStamp.SetActive(true);
        canvasMain.SetActive(false);        
    }

    public void ButtonToBack()
    {
        canvasStamp.SetActive(false);
        canvasMain.SetActive(true);
    }

    public void StampOnOff()
    {
        if (stampNum == 1) { check01.SetActive(true); }
        else if (stampNum == 2) { check02.SetActive(true); }
        else if (stampNum == 3) { check03.SetActive(true); }
        else if (stampNum == 4) { check04.SetActive(true); }
        else if (stampNum == 5) { check05.SetActive(true); }
        else if (stampNum == 6) { check06.SetActive(true); }
        else if (stampNum == 7) { check07.SetActive(true); }
    }
}
