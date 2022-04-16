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
        //"123456789��123456789��123456789��123456789��123456789��123456789��123456789��123456789��12" ������ �ؽ�Ʈ ����
        //\n���� ������ ����

        if (stampNum == 1) { text_info.text = $"�б� ������Ʈ �ϳ���"; }
        else if (stampNum == 2) { text_info.text = $"�����̿�"; }
        else if (stampNum == 3) { text_info.text = $"���� ���� �ϴµ�"; }
        else if (stampNum == 4) { text_info.text = $"��Ʋ Ÿ�� ��"; }
        else if (stampNum == 5) { text_info.text = $"�ư��=����"; }
        else if (stampNum == 6) { text_info.text = $"�����ϴµ�"; }
        else if (stampNum == 7) { text_info.text = $"�н�\n�Դµ�"; }
    }
}
