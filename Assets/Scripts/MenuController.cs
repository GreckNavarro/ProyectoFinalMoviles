using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameEvent OnLeave;

    public void Leave()
    {
        OnLeave.Raise();
    }
    public void Play()
    {
        SceneManager.LoadScene("SceneGame");
    }

    public void OnLeaveAction()
    {
        Debug.Log("Sali");
        Application.Quit();
    }
}
