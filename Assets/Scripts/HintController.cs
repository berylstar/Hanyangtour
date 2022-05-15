using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintController : MonoBehaviour
{
    public GameObject img1, img2, img3, img4, img5, img6, img7;

    public void ButtonPin1()
    {
        img1.SetActive(true);
        img2.SetActive(false);
        img3.SetActive(false);
        img4.SetActive(false);
        img5.SetActive(false);
        img6.SetActive(false);
        img7.SetActive(false);
    }

    public void ButtonPin2()
    {
        img1.SetActive(false);
        img2.SetActive(true);
        img3.SetActive(false);
        img4.SetActive(false);
        img5.SetActive(false);
        img6.SetActive(false);
        img7.SetActive(false);
    }

    public void ButtonPin3()
    {
        img1.SetActive(false);
        img2.SetActive(false);
        img3.SetActive(true);
        img4.SetActive(false);
        img5.SetActive(false);
        img6.SetActive(false);
        img7.SetActive(false);
    }

    public void ButtonPin4()
    {
        img1.SetActive(false);
        img2.SetActive(false);
        img3.SetActive(false);
        img4.SetActive(true);
        img5.SetActive(false);
        img6.SetActive(false);
        img7.SetActive(false);
    }

    public void ButtonPin5()
    {
        img1.SetActive(false);
        img2.SetActive(false);
        img3.SetActive(false);
        img4.SetActive(false);
        img5.SetActive(true);
        img6.SetActive(false);
        img7.SetActive(false);
    }

    public void ButtonPin6()
    {
        img1.SetActive(false);
        img2.SetActive(false);
        img3.SetActive(false);
        img4.SetActive(false);
        img5.SetActive(false);
        img6.SetActive(true);
        img7.SetActive(false);
    }

    public void ButtonPin7()
    {
        img1.SetActive(false);
        img2.SetActive(false);
        img3.SetActive(false);
        img4.SetActive(false);
        img5.SetActive(false);
        img6.SetActive(false);
        img7.SetActive(true);
    }
}
