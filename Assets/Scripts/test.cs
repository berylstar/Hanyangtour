using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject[] img;

    public int iii = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (iii == img.Length)
        {
            iii = 0;
        }
    }

    public void ButtonTest()
    {
        for (int i = 0; i < img.Length; i++)
        {
            img[i].SetActive(false);
        }

        img[iii].SetActive(true);
        iii++;
    }
}
