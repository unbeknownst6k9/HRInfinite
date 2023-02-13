using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Purpose: This class controls the behaviour of the phone UI
 */
public class PhoneUIController : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private GameObject PhoneMainPage;
    [SerializeField]
    private GameObject PhoneLoadingPage;
    [SerializeField]
    private GameObject PhoneSearchPage;

    private IEnumerator counter;
    #endregion

    #region Methods
    /*
     * TODO:
     * 1. turn on the loading screen
     * 2. count down the time
     * 3. once time is count update the information UI
     */
    private void Awake()
    {
        GameManager.Instance.OnDayEndCallBack += EndDay;
    }



    public void CheckResume()
    {
        PhoneLoadingPage.SetActive(true);
        counter = LoadingScreen();
        StartCoroutine(counter);
    }

    public void EndDay()
    {
        if (counter != null) StopCoroutine(counter);
        PhoneSearchPage.SetActive(false);
        PhoneLoadingPage.SetActive(false);

    }

    public void endDayButton()
    {
        
        GameManager.Instance.OnDayEndCallBack.Invoke();
    }

    private IEnumerator LoadingScreen()
    {
        PhoneSearchPage.SetActive(true);
        var seconds = GameManager.Instance.getPhoneLoadingTime();
        while (seconds > 0)
        {
            
            seconds -= Time.deltaTime;
            yield return null;
        }
        PhoneSearchPage.GetComponent<PhoneSearchPageUI>().updateUI();
        PhoneLoadingPage.SetActive(false);
    }
    #endregion
}
