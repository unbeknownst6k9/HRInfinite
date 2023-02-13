using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockControllerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject resultScreen;

    public RectTransform needle;
    private IEnumerator needleAnimation;

    private void Awake()
    {
        var resultUI = resultScreen.GetComponent<ResultScreenUI>();
        needleAnimation = startNeedleAnimation();
        GameManager.Instance.OnDayStartCallBack += startClock;
        GameManager.Instance.OnDayEndCallBack += resetClock;
        GameManager.Instance.OnDayEndCallBack += clockEndDay;
        GameManager.Instance.OnDayEndCallBack += resultUI.updateUI;
        GameManager.Instance.OnDayEndCallBack += resultUI.endDay;
        GameManager.Instance.OnEndGameCallBack += resetClock;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ClockControllerUI: Object Activated");
        //startClock();
    }

    private void OnEnable()
    {
        //resetClock();
        //StartCoroutine(needleAnimation);
    }


    private void resetClock()
    {
        //reset position
        StopCoroutine(needleAnimation);
        needle.rotation = Quaternion.Euler(0, 0, 0);
        
    }

    private void clockEndDay()
    {
        resultScreen.SetActive(true);
    }

    private void startClock()
    {
        needleAnimation = startNeedleAnimation();
        Debug.Log("Clock Controller UI: start clock");
        //StartCoroutine(checkActivation());
        while (!gameObject.activeSelf)
        {
            Debug.Log("Clock not activated");
        }
        StartCoroutine(needleAnimation);
    }

    private IEnumerator startNeedleAnimation()
    {
        var angle = 0.0f;
        var rotateAngle = 0.0f;
        while (angle > -360)
        {
            //-Time.deltaTime * GameManager.Instance.getDayDuration()
            rotateAngle = ( Time.deltaTime * 360/ GameManager.Instance.getDayDuration());
            angle -= rotateAngle;

            needle.Rotate(new Vector3(0, 0, -rotateAngle));
            yield return null;
        }
        GameManager.Instance.OnDayEndCallBack.Invoke();
        
    }

    public void pauseClock()
    {
        StopCoroutine(needleAnimation);
    }

    public void resumeClock()
    {
        StartCoroutine(needleAnimation);
    }
}
