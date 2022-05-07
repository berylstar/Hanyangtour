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
        if (stampNum == 1) { text_info.text = $"ERICA ķ�۽���\n������Ʈ �ϳ��� !\n���� �տ� �ִ� ���ڻ���\n��� �Ϳ���\nĳ���Ͱ� �Ǿ���"; }                                 //�ϳ���
        else if (stampNum == 2) { text_info.text = $"Ŀ�ٶ� ���ڻ���\n������ �ִ� ���� !\n�����, �л�ó�� ��ġ���־�.\n���𸶴� ���ϴ�\n���ڻ��� Ȯ���غ�"; }                //����
        else if (stampNum == 3) { text_info.text = $"�̰��� �������ǰ� ����Ǵ� ���۷��� Ȧ !\n��������� ����\n���� �߰��翡����\n��û����, ���� ��簡 ������ ������."; }        //���۷���Ȧ
        else if (stampNum == 4) { text_info.text = $"�̰��� ��Ʋ������ �ݶ��� �ռ����\n�ݶ� �� ���� ������ �Բ� ��Ʋ������ ��ٸ��� ���̶�� ���� ��Ʋ���̾� !"; }            //��Ʋ��
        else if (stampNum == 5) { text_info.text = $"�б� ���� �Ա�����\n�����ִ� �ư�� !\nERICA ķ�۽���\n��¡ �� �ϳ���.\n�̻� �߰� �տ���\n���� �ѹ� ����"; }        //�ư��
        else if (stampNum == 6) { text_info.text = $"������ ������ �� �ִ� �м� ������ !\n�������� 24�ð� �����ϰ� �־�.\n1���� �κ� ���� ī�䵵 ��ܺ���"; }              //����
        else if (stampNum == 7) { text_info.text = $"������, �л��Ĵ�\n����, ��ü��, ����\nī��, Ǫ����Ʈ\n���Ƹ������\n���°� ����\n�������̾�"; }
    }
}
