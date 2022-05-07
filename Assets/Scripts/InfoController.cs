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
        if (stampNum == 1) { text_info.text = $"ERICA 캠퍼스의\n마스코트 하냥이 !\n본관 앞에 있는 사자상이\n밝고 귀여운\n캐릭터가 되었지"; }                                 //하냥이
        else if (stampNum == 2) { text_info.text = $"커다란 사자상이\n세워져 있는 본관 !\n총장실, 학생처가 위치해있어.\n시즌마다 변하는\n사자상을 확인해봐"; }                //본관
        else if (stampNum == 3) { text_info.text = $"이곳은 대형강의가 진행되는 컨퍼런스 홀 !\n교양수업은 물론\n넓은 중강당에서는\n초청강연, 여러 행사가 열리는 곳이지."; }        //컨퍼런스홀
        else if (stampNum == 4) { text_info.text = $"이곳은 셔틀버스와 콜라의 합성어로\n콜라 한 잔의 여유와 함께 셔틀버스를 기다리는 곳이라는 뜻의 셔틀콕이야 !"; }            //셔틀콕
        else if (stampNum == 5) { text_info.text = $"학교 정문 입구부터\n볼수있는 아고라 !\nERICA 캠퍼스의\n상징 중 하나지.\n이쁜 야경 앞에서\n사진 한번 찍어보자"; }        //아고라
        else if (stampNum == 6) { text_info.text = $"열심히 공부할 수 있는 학술 정보관 !\n열람실은 24시간 개방하고 있어.\n1층의 로봇 무인 카페도 즐겨보자"; }              //학정
        else if (stampNum == 7) { text_info.text = $"편의점, 학생식당\n은행, 우체국, 서점\n카페, 푸드코트\n동아리방까지\n없는게 없는\n복지관이야"; }
    }
}
