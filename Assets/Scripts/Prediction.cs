using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;
using System.Linq;
using System;
using OpenCvSharp;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class Prediction : MonoBehaviour
{
    public Texture2D texture;
    public NNModel modelAsset;
    private Model _runtimeModel;
    private IWorker _engine;

    //public GameObject CameraM;

    public Text text1;      //객체 존재 확률
    public Text text2;      //객체 존재 T/F
    public Text text3;      //어떤 객체인가
    public Text text4;      //작동 로그 텍스트
                            //이것들은 나중엔 없애도 됨
    public Text text_WhatIsThis;

    public static float x0;
    public static float x1;
    public static float x2;
    public static float x3;
    public static float x4;     //존재확률
    public static float x5;     //하냥이
    public static float x6;     //본관
    public static float x7;     //컨퍼런스홀
    public static float x8;     //셔틀콕
    public static float x9;     //아고라
    public static float x10;    //학정
    public static float x11;    //복지관

    public int classNum;
    public static bool isExist;

    public AudioSource SoundCamera;

    /*
    [System.Serializable]
    public struct Predictions
    {
        public int predictedValue;
        public float[] predicted;

        public void SetPrediction(Tensor t)
        {
            predicted = t.AsFloats();
            predictedValue = System.Array.IndexOf(predicted, predicted.Max());
            Debug.Log(predictedValue);
        }
    }
    */

    //public Predictions predictions;

    void Start()
    {
        _runtimeModel = ModelLoader.Load(modelAsset);

        try
        {
            _engine = WorkerFactory.CreateWorker(_runtimeModel, WorkerFactory.Device.CPU);
            print($"{WorkerFactory.Device.CPU}");
            text4.text = $"{WorkerFactory.Device.CPU}";
        }
        catch (System.Exception)
        {
            print("엔진멸망");
            text4.text = "엔진멸망";
            _engine = WorkerFactory.CreateWorker(_runtimeModel, WorkerFactory.Device.GPU);
        }

        //predictions = new Predictions();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
            GETS();
        }
    }

    private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);
        float incX = (1.0f / (float)targetWidth);
        float incY = (1.0f / (float)targetHeight);

        for (int i = 0; i < result.height; ++i)
        {
            for (int j = 0; j < result.width; ++j)
            {
                Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                result.SetPixel(j, i, newColor);
            }
        }

        result.Apply();
        return result;
    }

    public void GETS()
    {
        StartCoroutine(Gettt());
        SoundCamera.Play();
    }

    IEnumerator Gettt() {
        print("GETS!");
        //if (Input.GetKeyDown(KeyCode.Space)){
        //texture = Projection.jjj;
        //RenderTexture render = CameraM.GetComponent<Camera>().targetTexture;
        //print(render);
        //texture = new Texture2D(render.width, render.height);

        try
        {
            //texture = GetTextureFromCamera(CameraM.GetComponent<Camera>());
            //texture = ScreenCapture.CaptureScreenshotAsTexture();

           texture= GetTextureFromCamera(Camera.main);

            try
            {
                //Mat frame = OpenCvSharp.Unity.TextureToMat(texture);
                //Cv2.Resize(frame, frame, new Size(640, 640));
                //texture = OpenCvSharp.Unity.MatToTexture(frame);

                texture = ScaleTexture(texture, 640, 640);
                print(texture);
                text4.text = $"변환성공";
            }
            catch (Exception ex)
            {
                text4.text = $"{ex.ToString()}";
            }

            //text4.text = $"촬영성공";
        }
        catch (Exception)
        {
            text4.text = $"촬영실패";
        }

        // texture=GetTextureFromCamera(CameraM.GetComponent<Camera>());

        var inputX = new Tensor(texture, 3);
        Tensor OutputY = _engine.Execute(inputX).PeekOutput();
        inputX.Dispose();
        //predictions.SetPrediction(OutputY);


        //선택과 집중

        float temp0 = 0;
        int indexofob = -1;
        int indexofob1 = -1;
        int indexofob2 = -1;

        for (int i = 0; i < 25200; i++)
        {
            float temp1 = (float)OutputY[0, 0, 4, i];
            if (temp0 < temp1)
            {
                temp0 = temp1;

                indexofob2 = indexofob1;    //전전 존재확률의 최댓값의 인덱스 저장
                indexofob1 = indexofob;     // 직전 존재확률의 최댓값의 인덱스 저장
                indexofob = i;              //현재 존재확률의 최댓값의 인덱스 저장
            }
        }

        x0 = OutputY[0, 0, 0, indexofob];
        x1 = OutputY[0, 0, 1, indexofob];
        x2 = OutputY[0, 0, 2, indexofob];
        x3 = OutputY[0, 0, 3, indexofob];

        //NMS 구현
        x4 = (OutputY[0, 0, 4, indexofob] + OutputY[0, 0, 4, indexofob1] + OutputY[0, 0, 4, indexofob2]) / 3;
        x5 = (OutputY[0, 0, 5, indexofob] + OutputY[0, 0, 5, indexofob1] + OutputY[0, 0, 5, indexofob2]) / 3;
        x6 = (OutputY[0, 0, 6, indexofob] + OutputY[0, 0, 6, indexofob1] + OutputY[0, 0, 6, indexofob2]) / 3;
        x7 = (OutputY[0, 0, 7, indexofob] + OutputY[0, 0, 7, indexofob1] + OutputY[0, 0, 7, indexofob2]) / 3;
        x8 = (OutputY[0, 0, 8, indexofob] + OutputY[0, 0, 8, indexofob1] + OutputY[0, 0, 8, indexofob2]) / 3;
        x9 = (OutputY[0, 0, 9, indexofob] + OutputY[0, 0, 9, indexofob1] + OutputY[0, 0, 9, indexofob2]) / 3;
        x10 = (OutputY[0, 0, 10, indexofob] + OutputY[0, 0, 10, indexofob1] + OutputY[0, 0, 10, indexofob2]) / 3;
        x11 = (OutputY[0, 0, 11, indexofob] + OutputY[0, 0, 11, indexofob1] + OutputY[0, 0, 11, indexofob2]) / 3;
        //['hanayang','mainhall','conference','shuttle','agora','library','welfare']

        if (x4 > 0.009)
        {
            isExist = true;
            float[] temp_float = { x5, x6, x7, x8, x9, x10, x11 };
            float tempindex = -1;
            int tempindex2 = -1;

            for (int iy = 0; iy < 7; iy++)
            {
                if (tempindex < temp_float[iy])
                {
                    tempindex = temp_float[iy];
                    tempindex2 = iy + 1;        //+1 해서 0 일때 인식하는걸 방지
                }
            }
            classNum = tempindex2;
        }
        else
        {
            isExist = false;
            classNum = 0;
        }

        print("위치좌표 1: " + x0);
        print("위치좌표 2: " + x1);
        print("위치좌표 3: " + x2);
        print("위치좌표 4: " + x3);
        print("객체 여부 확률: " + x4);
        //print("class0일 확률: : " + x5);
        //print("class1일 확률: " + x6);

        text1.text = $"객체 존재 : {x4}";
        text2.text = $"존재? : {isExist}";

        if (classNum == 1) { text3.text = $"class뭘까 : 하냥이"; }
        else if (classNum == 2) { text3.text = $"class뭘까 : 본관"; }
        else if (classNum == 3) { text3.text = $"class뭘까 : 컨퍼런스홀"; }
        else if (classNum == 4) { text3.text = $"class뭘까 : 셔틀콕"; }
        else if (classNum == 5) { text3.text = $"class뭘까 : 아고라"; }
        else if (classNum == 6) { text3.text = $"class뭘까 : 학정"; }
        else if (classNum == 7) { text3.text = $"class뭘까 : 복지관"; }
        else { text3.text = $"class뭘까 : 몰루"; }

        if (classNum == 1) { text_WhatIsThis.text = $"하냥이"; }
        else if (classNum == 2) { text_WhatIsThis.text = $"본관"; }
        else if (classNum == 3) { text_WhatIsThis.text = $"컨퍼런스홀"; }
        else if (classNum == 4) { text_WhatIsThis.text = $"셔틀콕"; }
        else if (classNum == 5) { text_WhatIsThis.text = $"아고라"; }
        else if (classNum == 6) { text_WhatIsThis.text = $"학술정보관"; }
        else if (classNum == 7) { text_WhatIsThis.text = $"복지관"; }
        else { text_WhatIsThis.text = $"???"; }

        yield return 0;
    }

    private static Texture2D GetTextureFromCamera(Camera mCamera)
    {
        UnityEngine.Rect rect = new UnityEngine.Rect(0, 0, mCamera.pixelWidth, mCamera.pixelHeight);
        RenderTexture renderTexture = new RenderTexture(mCamera.pixelWidth, mCamera.pixelHeight, 24);
        Texture2D screenShot = new Texture2D(mCamera.pixelWidth, mCamera.pixelHeight, TextureFormat.RGBA32, false);

        mCamera.targetTexture = renderTexture;
        mCamera.Render();

        RenderTexture.active = renderTexture;

        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();

        mCamera.targetTexture = null;
        RenderTexture.active = null;
        return screenShot;

        //Mat frame = OpenCvSharp.Unity.TextureToMat(screenShot);
        //Cv2.Resize(frame, frame, new Size(640, 640));
        //screenShot = OpenCvSharp.Unity.MatToTexture(frame);
    }

    private void OnDestroy()
    {
        _engine.Dispose();
    }
}