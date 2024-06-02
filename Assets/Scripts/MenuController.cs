using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameEvent OnLeave;

    public void Leave()
    {
        OnLeave.Raise();
    }
    public void OnLeaveAction()
    {
        Debug.Log("Sali");
        Application.Quit();
    }
}
