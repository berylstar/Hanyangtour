using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampController : MonoBehaviour
{
    public GameObject objjj;

    public int numm;

    public GameObject check01;
    public GameObject check02;
    public GameObject check03;
    public GameObject check04;
    public GameObject check05;
    public GameObject check06;
    public GameObject check07;

    void Start()
    {

    }   

    void Update()
    {

    }

    public void ButtonToStamp()
    {
        this.gameObject.SetActive(true);
        objjj.SetActive(false);

        Prediction pred = objjj.GetComponent<Prediction>();
        numm = pred.classNum;

        if(numm == 1) { check01.SetActive(true); }
        else if(numm == 2) { check02.SetActive(true); }
        else if(numm == 3) { check03.SetActive(true); }
        else if (numm == 4) { check04.SetActive(true); }
        else if (numm == 5) { check05.SetActive(true); }
        else if (numm == 6) { check06.SetActive(true); }
        else if (numm == 7) { check07.SetActive(true); }
    }

    public void ButtonToBack()
    {
        this.gameObject.SetActive(false);
        objjj.SetActive(true);
    }
}
