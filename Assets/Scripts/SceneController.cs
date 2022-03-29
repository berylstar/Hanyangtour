using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonStart()
    {
        SceneManager.LoadScene("RuleScene");
    }

    public void ButtonExit()
    {
        Application.Quit();
    }

    public void ButtonNext()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ButtonStamp()
    {
        SceneManager.LoadScene("StampScene");
    }

    public void ButtonBack()
    {
        SceneManager.LoadScene("MainScene");
    }
}
