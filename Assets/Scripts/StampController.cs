using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampController : MonoBehaviour
{
    public Prediction prediction;

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
        check01.SetActive(true);
    }
}
