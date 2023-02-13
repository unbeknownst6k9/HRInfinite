using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{

    public void StartGame()
    {
        GameManager.Instance.OnStartGameCallBack.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
