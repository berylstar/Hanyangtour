using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;
using System.Linq;
using System;
using OpenCvSharp;
using UnityEngine.UI;
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


          }
          catch (System.Exception)
          {
              print("¿£Áø¸ê¸Á");
              _engine = WorkerFactory.CreateWorker(_runtimeModel, WorkerFactory.Device.GPU);
          }
        
        predictions = new Predictions();

    }
    public static List<List<float>> GetCandidate(float[] pred, int[] pred_dim, float pred_thresh = 0.25f)
    {
        List<List<float>> candidate = new List<List<float>>();
        for (int batch = 0; batch < pred_dim[0]; batch++)
        {
            for (int cand = 0; cand < pred_dim[1]; cand++)
            {
                int score = 4;  // objectness score
                int idx1 = (batch * pred_dim[1] * pred_dim[2]) + cand * pred_dim[2];
                int idx2 = idx1 + score;
                var value = pred[idx2];
                if (value > pred_thresh)
                {
                    List<float> tmp_value = new List<float>();
                    for (int i = 0; i < pred_dim[2]; i++)
                    {
                        int sub_idx = idx1 + i;
                        tmp_value.Add(pred[sub_idx]);
                    }
                    candidate.Add(tmp_value);
                }
            }
        }
        return candidate;
    }
    void Update() {

      
    
    }

    // Update is called once per frame
    public void GETS()
        {
        
          //  if (Input.GetKeyDown(KeyCode.Space))
          //  {
            print("haha");
            //texture = Projection.jjj;
            //RenderTexture render = CameraM.GetComponent<Camera>().targetTexture;
            //print(render);
            //texture = new Texture2D(render.width, render.height);
            texture=GetTextureFromCamera(CameraM.GetComponent<Camera>());
            var inputX = new Tensor(texture, 3);
                Tensor OutputY = _engine.Execute(inputX).PeekOutput();
                inputX.Dispose();

            /////// ¼±ÅÃ°ú ÁýÁß
            var x0 = OutputY[0,0,0,0];
            var x1 = OutputY[0,0,1,0];
            var x2 = OutputY[0,0,2,0];
            var x3 = OutputY[0,0,3,0];
            var x4 = OutputY[0,0,4,0];
            var x5 = OutputY[0,0,5,0];
            var x6 = OutputY[0,0,6,0];
            print("À§Ä¡ÁÂÇ¥ 1: "+ x0);
            print("À§Ä¡ÁÂÇ¥ 2: "+ x1);
            print("À§Ä¡ÁÂÇ¥ 3: "+ x2);
            print("À§Ä¡ÁÂÇ¥ 4: "+ x3);
            print("°´Ã¼ ¿©ºÎ È®·ü: "+ x4);
            print("class0ÀÏ È®·ü: : "+ x5);
            print("class1ÀÏ È®·ü: "+ x6);

        text1.text = $"°´Ã¼ Á¸ÀçÈ®·ü {x4}";
        text2.text = $"class0 È®·ü: {x5}";
        text3.text = $"class1È®·ü: {x6}";


        //////



        //predictions.SetPrediction(OutputY);

        //       }
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
