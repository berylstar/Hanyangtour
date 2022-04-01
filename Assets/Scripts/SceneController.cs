using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int nummm;

    public GameObject objjjj;

    public GameObject sceneController;

    void Start()
    {
        
    }


    void Update()
    {
        Prediction pred = objjjj.GetComponent<Prediction>();
        nummm = pred.classNum;
    }


    public void ButtonToMain()
    {
        SceneManager.LoadScene("MainScene");

        var objs = FindObjectsOfType<SceneController>();
        if(objs.Length == 1)
        {
            DontDestroyOnLoad(sceneController);
        }
        else
        {
            Destroy(sceneController);
        }

        Prediction pred = objjjj.GetComponent<Prediction>();
        nummm = pred.classNum;
    }

    public void ButtonToRule()
    {
        SceneManager.LoadScene("RuleScene");

        var objs = FindObjectsOfType<SceneController>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(sceneController);
        }
        else
        {
            Destroy(sceneController);
        }
    }

    public void ButtonToStamp()
    {
        SceneManager.LoadScene("StampScene");

        var objs = FindObjectsOfType<SceneController>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(sceneController);
        }
        else
        {
            Destroy(sceneController);
        }
    }

    public void ButtonToExit()
    {
        Application.Quit();
    }
}
