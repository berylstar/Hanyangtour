using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StampController : MonoBehaviour
{
    public GameObject canvasMain;
    public GameObject canvasStamp;

    private int stampNum;

    [Header ("StampCheck")]
    public GameObject check01;  //하냥이
    public GameObject check02;  //본관
    public GameObject check03;  //컨퍼런스홀
    public GameObject check04;  //셔틀콕
    public GameObject check05;  //아고라
    public GameObject check06;  //학정
    public GameObject check07;  //복지관

    private bool flag1, flag2, flag3, flag4, flag5, flag6, flag7;
    private bool flag8 = true;

    void Start()
    {

    }   

    void Update()
    {
        Prediction pred = canvasMain.GetComponent<Prediction>();
        stampNum = pred.classNum;

        StampOn();
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

    private void StampOn()
    {
        if (stampNum == 1) { check01.SetActive(true); flag1 = true; }
        else if (stampNum == 2) { check02.SetActive(true); flag2 = true; }
        else if (stampNum == 3) { check03.SetActive(true); flag3 = true; }
        else if (stampNum == 4) { check04.SetActive(true); flag4 = true; }
        else if (stampNum == 5) { check05.SetActive(true); flag5 = true; }
        else if (stampNum == 6) { check06.SetActive(true); flag6 = true; }
        else if (stampNum == 7) { check07.SetActive(true); flag7 = true; }

        if(flag1 == true && flag2 == true && flag3 == true && flag4 == true && flag5 == true && flag6 == true && flag7 == true && flag8 == true)
        {
            print("ALL DETECTED");
            flag8 = false;

            //buttonEnding.SetActive(true);
        }
    }
}
