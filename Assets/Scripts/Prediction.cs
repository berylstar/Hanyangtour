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
    public GameObject CameraM;
    private Model _runtimeModel;
    private IWorker _engine;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;

    public static float x0;
    public static float x1;
    public static float x2;
    public static float x3;
    public static float x4;
    public static float x5;
    public static float x6;
    public static float x7;
    public static float x8;
    public static float x9;
    public static float x10;
    public static float x11;
 


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
    // Start is called before the first frame update

    public Predictions predictions;
    void Start()
    {
        
        
        _runtimeModel = ModelLoader.Load(modelAsset);

          try
          {
              _engine = WorkerFactory.CreateWorker(_runtimeModel, WorkerFactory.Device.CPU);
            print( $"{WorkerFactory.Device.CPU}");
            text4.text = $"{WorkerFactory.Device.CPU}";

        }
          catch (System.Exception)
          {
              print("�������");
            text4.text = "�������";
              _engine = WorkerFactory.CreateWorker(_runtimeModel, WorkerFactory.Device.GPU);
          }
        
        predictions = new Predictions();

    }
    
    void Update() {

      
    
    }

    // Update is called once per frame
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

        //////



        //predictions.SetPrediction(OutputY);

        //       }
    }

    IEnumerator Gettt() {
        //  if (Input.GetKeyDown(KeyCode.Space))
        //  {
        print("GETS!");
        //texture = Projection.jjj;
        //RenderTexture render = CameraM.GetComponent<Camera>().targetTexture;
        //print(render);
        //texture = new Texture2D(render.width, render.height);
        try
        {
            // texture = GetTextureFromCamera(CameraM.GetComponent<Camera>());
            ///

            texture = ScreenCapture.CaptureScreenshotAsTexture();
            try
            {
                //  Mat frame = OpenCvSharp.Unity.TextureToMat(texture);

                //Cv2.Resize(frame, frame, new Size(640, 640));
                //texture = OpenCvSharp.Unity.MatToTexture(frame);
                texture = ScaleTexture(texture, 640, 640);
                print(texture);
                text4.text = $"��ȯ����";
            }
            catch (Exception ex)
            {

                text4.text = $"{ex.ToString()}";
            }
            ///


            //    text4.text = $"�Կ�����";
        }
        catch (Exception)
        {
            text4.text = $"�Կ�����";

        }

        // texture=GetTextureFromCamera(CameraM.GetComponent<Camera>());
        var inputX = new Tensor(texture, 3);
        Tensor OutputY = _engine.Execute(inputX).PeekOutput();
        inputX.Dispose();

        /////// ���ð� ����



        float temp0 = 0;
        int indexofob = -1;
        for (int i = 0; i < 25200; i++)
        {
            float temp1 = (float)OutputY[0, 0, 4, i];
            if (temp0 < temp1)
            {
                temp0 = temp1;
                indexofob = i;
            }
        }
        x0 = OutputY[0, 0, 0, indexofob];
        x1 = OutputY[0, 0, 1, indexofob];
        x2 = OutputY[0, 0, 2, indexofob];
        x3 = OutputY[0, 0, 3, indexofob];
        x4 = OutputY[0, 0, 4, indexofob];   //����Ȯ��
        x5 = OutputY[0, 0, 5, indexofob];   // �ϳ���
        x6 = OutputY[0, 0, 6, indexofob];   //����
        x7 = OutputY[0, 0, 7, indexofob];   //���۷���Ȧ
        x8 = OutputY[0, 0, 8, indexofob];   //��Ʋ��
        x9 = OutputY[0, 0, 9, indexofob];   //�ư��
        x10 = OutputY[0, 0, 10, indexofob]; //����
        x11 = OutputY[0, 0, 11, indexofob]; //������
        //['hanayang','mainhall','conference','shuttle','erica','library','welfare']

        print("��ġ��ǥ 1: " + x0);
        print("��ġ��ǥ 2: " + x1);
        print("��ġ��ǥ 3: " + x2);
        print("��ġ��ǥ 4: " + x3);
        print("��ü ���� Ȯ��: " + x4);
        print("class0�� Ȯ��: : " + x5);
        print("class1�� Ȯ��: " + x6);

        text1.text = $"��ü ���� {x4}";
        text2.text = $"class0 Ȯ��: {x5}";
        text3.text = $"class1Ȯ��: {x6}";







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
        Mat frame = OpenCvSharp.Unity.TextureToMat(screenShot);
        Cv2.Resize(frame, frame, new Size(640, 640));
        screenShot = OpenCvSharp.Unity.MatToTexture(frame);
        return screenShot;
    }

    private void OnDestroy()
    {
        _engine.Dispose();
    }


}
