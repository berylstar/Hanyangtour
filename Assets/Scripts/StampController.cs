using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampController : MonoBehaviour
{
    public GameObject predictionObj;

    public int stampNumm;

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

    }

    public void ButtonToStamp()
    {
        this.gameObject.SetActive(true);
        predictionObj.SetActive(false);

        Prediction pred = predictionObj.GetComponent<Prediction>();
        stampNumm = pred.classNum;

        //������ Ű�����
        if(stampNumm == 1) { check01.SetActive(true); }
        else if(stampNumm == 2) { check02.SetActive(true); }
        else if(stampNumm == 3) { check03.SetActive(true); }
        else if (stampNumm == 4) { check04.SetActive(true); }
        else if (stampNumm == 5) { check05.SetActive(true); }
        else if (stampNumm == 6) { check06.SetActive(true); }
        else if (stampNumm == 7) { check07.SetActive(true); }
    }

    public void ButtonToBack()
    {
        this.gameObject.SetActive(false);
        predictionObj.SetActive(true);
    }
}
