using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultScreenUI : MonoBehaviour
{
    public Animator FadeAnimator;

    public float AnimationTime;

    public Animator FontShrink;

    public float FontShrinkAnimationTime;

    public Animator buttonAnimation;

    public float buttonAnimationTime;

    [SerializeField]
    private TextMeshProUGUI dayText;

    [SerializeField]
    private TextMeshProUGUI resumeCheckText;

    [SerializeField]
    private TextMeshProUGUI nextRoundText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI warningText;

    public void updateUI()
    {
        var round = GameManager.Instance.getPlayerRound();
        dayText.text = round.dayCount.ToString();
        resumeCheckText.text = round.resumeCount.ToString();
        nextRoundText.text = round.dayUntilNextRound.ToString();
        scoreText.text = round.currentScore.ToString("0.0") + " / " + round.maxScore.ToString("0.0");
        //if (round.warning >= round.maxWarning) warningText.color = Color.red;
        warningText.text = round.warning.ToString() + " / " + round.maxWarning.ToString();
    }

    private IEnumerator fadeIn()
    {
        var round = GameManager.Instance.getPlayerRound();
        bool lastWarning = round.warning >= round.maxWarning;
        gameObject.SetActive(true);
        FadeAnimator.SetBool("FadeState", true);
        warningText.gameObject.SetActive(!lastWarning);
        //for button
        buttonAnimation.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        buttonAnimation.gameObject.SetActive(false);

        yield return new WaitForSeconds(AnimationTime);
        Debug.Log("Trigger warning font");
        warningText.gameObject.SetActive(true);
        if (lastWarning)
        {
            warningText.color = Color.red;
            FontShrink.SetTrigger("warningState");
        }
        
        yield return new WaitForSeconds(lastWarning? FontShrinkAnimationTime:0f);
        buttonAnimation.gameObject.SetActive(true);
        buttonAnimation.SetTrigger("showButton");

    }

    private IEnumerator fadeOut(GameObject gameOverUI)
    {
        var GM = GameManager.Instance;
        Debug.Log($"<{typeof(ResultScreenUI)}> start fade");
        if (GM.getPlayerRound().maxRound <= GM.getPlayerRound().currentRound && GM.getPlayerRound().dayUntilNextRound == 0)
        {
            //call endgame true
            Debug.Log($"<{typeof(ResultScreenUI)}> is winning");
            gameOverUI.SetActive(true);
            GM.OnGameOverCallBack.Invoke(true);
        }
        else if (GM.getPlayerRound().warning >= GM.getPlayerRound().maxWarning)
        {
            //call endgame false
            Debug.Log($"<{typeof(ResultScreenUI)}> is losing");
            gameOverUI.SetActive(true);
            GM.OnGameOverCallBack.Invoke(false);
        }
        else
        {
            Debug.Log($"<{typeof(ResultScreenUI)}> is new day");
            FadeAnimator.SetBool("FadeState", false);
            yield return new WaitForSeconds(AnimationTime);
            GameManager.Instance.OnDayStartCallBack.Invoke();
        }
        
        gameObject.SetActive(false);
    }

    public void endDay()
    {

        StartCoroutine(fadeIn());
    }

    public void newDay(GameObject gameOverUI)
    {
        StartCoroutine(fadeOut(gameOverUI));
    }


    private string removeR(string text)
    {
        return text.Replace("\r", "");
    }
}
