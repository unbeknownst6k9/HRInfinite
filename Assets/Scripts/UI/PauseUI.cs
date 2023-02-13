using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public Animator FadeAnimator;
    public float AnimationTime;

    public void PauseGame()
    {
        gameObject.SetActive(true);
        StartCoroutine(startFade());
    }

    public void UnpauseGame()
    {
        StartCoroutine(EndFade());
    }

    public void returnToMenu()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        GameManager.Instance.OnEndGameCallBack.Invoke();
    }

    IEnumerator startFade()
    {
        FadeAnimator.SetBool("FadeState", true);
        yield return new WaitForSeconds(AnimationTime);
    }


    IEnumerator EndFade()
    {
        FadeAnimator.SetBool("FadeState", false);
        yield return new WaitForSeconds(AnimationTime);
        Debug.Log("end animation");
        gameObject.SetActive(false);
    }
}
