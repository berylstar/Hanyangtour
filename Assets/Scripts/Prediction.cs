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

    public Text text1;      //°´Ã¼ Á¸Àç È®·ü
    public Text text2;      //°´Ã¼ Á¸Àç T/F
    public Text text3;      //¾î¶² °´Ã¼ÀÎ°¡
    public Text text4;      //ÀÛµ¿ ·Î±× ÅØ½ºÆ®

    public static float x0;
    public static float x1;
    public static float x2;
    public static float x3;
    public static float x4;     //Á¸ÀçÈ®·ü
    public static float x5;     //ÇÏ³ÉÀÌ
    public static float x6;     //º»°ü
    public static float x7;     //ÄÁÆÛ·±½ºÈ¦
    public static float x8;     //¼ÅÆ²ÄÛ
    public static float x9;     //¾Æ°í¶ó
    public static float x10;    //ÇÐÁ¤
    public static float x11;    //º¹Áö°ü

    public int classNum;
    public static bool isExist;

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

    public Predictions predictions;

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
            print("¿£Áø¸ê¸Á");
            text4.text = "¿£Áø¸ê¸Á";
            _engine = WorkerFactory.CreateWorker(_runtimeModel, WorkerFactory.Device.GPU);
        }

        predictions = new Predictions();
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
                text4.text = $"º¯È¯¼º°ø";
            }
            catch (Exception ex)
            {
                text4.text = $"{ex.ToString()}";
            }

            //text4.text = $"ÃÔ¿µ¼º°ø";
        }
        catch (Exception)
        {
            text4.text = $"ÃÔ¿µ½ÇÆÐ";
        }

        // texture=GetTextureFromCamera(CameraM.GetComponent<Camera>());

        var inputX = new Tensor(texture, 3);
        Tensor OutputY = _engine.Execute(inputX).PeekOutput();
        inputX.Dispose();
        //predictions.SetPrediction(OutputY);


        //¼±ÅÃ°ú ÁýÁß

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

                indexofob2 = indexofob1;    //ÀüÀü Á¸ÀçÈ®·üÀÇ ÃÖ´ñ°ªÀÇ ÀÎµ¦½º ÀúÀå
                indexofob1 = indexofob;     // Á÷Àü Á¸ÀçÈ®·üÀÇ ÃÖ´ñ°ªÀÇ ÀÎµ¦½º ÀúÀå
                indexofob = i;              //ÇöÀç Á¸ÀçÈ®·üÀÇ ÃÖ´ñ°ªÀÇ ÀÎµ¦½º ÀúÀå
            }
        }

        x0 = OutputY[0, 0, 0, indexofob];
        x1 = OutputY[0, 0, 1, indexofob];
        x2 = OutputY[0, 0, 2, indexofob];
        x3 = OutputY[0, 0, 3, indexofob];

        //¿©±â¼­ºÎÅÍ À¯»ç NMS ±¸Çö
        x4 = (OutputY[0, 0, 4, indexofob] + OutputY[0, 0, 4, indexofob1] + OutputY[0, 0, 4, indexofob2]) / 3;
        x5 = (OutputY[0, 0, 5, indexofob] + OutputY[0, 0, 5, indexofob1] + OutputY[0, 0, 5, indexofob2]) / 3;
        x6 = (OutputY[0, 0, 6, indexofob] + OutputY[0, 0, 6, indexofob1] + OutputY[0, 0, 6, indexofob2]) / 3;
        x7 = (OutputY[0, 0, 7, indexofob] + OutputY[0, 0, 7, indexofob1] + OutputY[0, 0, 7, indexofob2]) / 3;
        x8 = (OutputY[0, 0, 8, indexofob] + OutputY[0, 0, 8, indexofob1] + OutputY[0, 0, 8, indexofob2]) / 3;
        x9 = (OutputY[0, 0, 9, indexofob] + OutputY[0, 0, 9, indexofob1] + OutputY[0, 0, 9, indexofob2]) / 3;
        x10 = (OutputY[0, 0, 10, indexofob] + OutputY[0, 0, 10, indexofob1] + OutputY[0, 0, 10, indexofob2]) / 3;
        x11 = (OutputY[0, 0, 11, indexofob] + OutputY[0, 0, 11, indexofob1] + OutputY[0, 0, 11, indexofob2]) / 3;
        //['hanayang','mainhall','conference','shuttle','erica','library','welfare']

        if (x4 > 0.1)
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
                    tempindex2 = iy + 1;        //+1 ÇØ¼­ 0 ÀÏ¶§ ÀÎ½ÄÇÏ´Â°É ¹æÁö
                }
            }
            classNum = tempindex2;
        }
        else
        {
            isExist = false;
            classNum = 0;
        }

        print("À§Ä¡ÁÂÇ¥ 1: " + x0);
        print("À§Ä¡ÁÂÇ¥ 2: " + x1);
        print("À§Ä¡ÁÂÇ¥ 3: " + x2);
        print("À§Ä¡ÁÂÇ¥ 4: " + x3);
        print("°´Ã¼ ¿©ºÎ È®·ü: " + x4);
        //print("class0ÀÏ È®·ü: : " + x5);
        //print("class1ÀÏ È®·ü: " + x6);

        text1.text = $"°´Ã¼ Á¸Àç : {x4}";
        text2.text = $"Á¸Àç? : {isExist}";

        if (classNum == 1) { text3.text = $"class¹»±î : ÇÏ³ÉÀÌ"; }
        else if (classNum == 2) { text3.text = $"class¹»±î : º»°ü"; }
        else if (classNum == 3) { text3.text = $"class¹»±î : ÄÁÆÛ·±½ºÈ¦"; }
        else if (classNum == 4) { text3.text = $"class¹»±î : ¼ÅÆ²ÄÛ"; }
        else if (classNum == 5) { text3.text = $"class¹»±î : ¾Æ°í¶ó"; }
        else if (classNum == 6) { text3.text = $"class¹»±î : ÇÐÁ¤"; }
        else if (classNum == 7) { text3.text = $"class¹»±î : º¹Áö°ü"; }
        else { text3.text = $"class¹»±î : ¸ô·ç"; }

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