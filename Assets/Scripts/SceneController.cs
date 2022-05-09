using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public AudioSource SoundButton;

    public void ButtonToIntro()
    {
        SceneManager.LoadScene("StartScene");
        SoundButton.Play();
    }

    public void ButtonToMain()
    {
        SceneManager.LoadScene("MainScene");
        SoundButton.Play();
    }

    public void ButtonToRule()
    {
        SceneManager.LoadScene("RuleScene");
        SoundButton.Play();
    }
    
    public void ButtonToExit()
    {
        Application.Quit();
        SoundButton.Play();
    }

    public void ButtonToEnd()
    {
        SceneManager.LoadScene("EndingScene");
        SoundButton.Play();
    }

    public void ButtonToFace()
    {
        SceneManager.LoadScene("FaceScene");
        SoundButton.Play();
    }
}