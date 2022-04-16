using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{
    public GameObject canvasMain;
    public Text text_info;

    private int stampNum;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Prediction pred = canvasMain.GetComponent<Prediction>();
        stampNum = pred.classNum;

        InfoOn();
    }

    private void InfoOn()
    {
        //"123456789ㄱ123456789ㄴ123456789ㄷ123456789ㄹ123456789ㅁ123456789ㅂ123456789ㅅ123456789ㅇ12" 이정도 텍스트 가능
        //\n으로 다음줄 가능

        if (stampNum == 1) { text_info.text = $"학교 마스코트 하냥이"; }
        else if (stampNum == 2) { text_info.text = $"본관이여"; }
        else if (stampNum == 3) { text_info.text = $"대형 강의 하는데"; }
        else if (stampNum == 4) { text_info.text = $"셔틀 타는 데"; }
        else if (stampNum == 5) { text_info.text = $"아고라=정문"; }
        else if (stampNum == 6) { text_info.text = $"공부하는데"; }
        else if (stampNum == 7) { text_info.text = $"학식\n먹는데"; }
    }
}
