using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ButtonToIntro()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ButtonToMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ButtonToRule()
    {
        SceneManager.LoadScene("RuleScene");
    }
    
    public void ButtonToExit()
    {
        Application.Quit();
    }

    public void ButtonToEnd()
    {
        SceneManager.LoadScene("EndingScene");
    }
}
