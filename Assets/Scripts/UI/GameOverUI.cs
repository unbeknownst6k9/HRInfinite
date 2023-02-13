using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [TextArea]
    public string WinningMessage;

    [TextArea]
    public string LosingMessage;

    [SerializeField]
    private TextMeshProUGUI OnScreenMessage;

    private void Awake()
    {
        GameManager.Instance.OnGameOverCallBack += setMessage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void returnToMainMenu()
    {
        GameManager.Instance.OnEndGameCallBack.Invoke();
        gameObject.SetActive(false);
    }


    private void setMessage(bool isWinning)
    {
        Debug.Log("Game Over Message");
        if (isWinning)
        {
            OnScreenMessage.text = WinningMessage;
        }
        else
        {
            OnScreenMessage.text = LosingMessage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
