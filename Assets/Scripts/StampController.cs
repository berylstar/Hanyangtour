using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampController : MonoBehaviour
{
    public GameObject predictionObj;

    public int stampNumm;

    public GameObject check01;  //하냥이
    public GameObject check02;  //본관
    public GameObject check03;  //컨퍼런스홀
    public GameObject check04;  //셔틀콕
    public GameObject check05;  //아고라
    public GameObject check06;  //학정
    public GameObject check07;  //복지관

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

        //스탬프 키고끄기
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
