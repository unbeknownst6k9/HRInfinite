using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DataStruct;
/*purpose: this is a controller for UI
 * 
 */
public class ResumeUIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI ageText;
    [SerializeField]
    private TextMeshProUGUI emailText;
    [SerializeField]
    private TextMeshProUGUI educationText;
    [SerializeField]
    private TextMeshProUGUI skillsText;
    [SerializeField]
    private GameObject NoResumeScreen;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnDayStartCallBack += restartScreen;
        gameManager.OnDayStartCallBack += gameStartSetup;
        updateUI(gameManager.currentResume);
    }

    private void gameStartSetup()
    {
        Debug.Log("Resume Controller UI Setup");
        updateUI(gameManager.currentResume);
    }

    public void updateUI(Resume r)
    {
        if (r.firstName != null) {
            nameText.text = removeR(r.firstName + " " + r.lastName);
            ageText.text = r.age.ToString();
            emailText.text = removeR(r.email);
            educationText.text = removeR(r.Educations[0].name);
            skillsText.text = removeR(r.Skills[0].skillName);
        }
        else
        {
            NoResumeScreen.SetActive(true);
        }
    }

    private void restartScreen()
    {
        NoResumeScreen.SetActive(false);
    }

    public void acceptResume()
    {
        var newResume = gameManager.getNewResume(true);
        updateUI(newResume);
    }

    public void refuseResume()
    {
        var newResume = gameManager.getNewResume(false);
        updateUI(newResume);
    }

    private string removeR(string text)
    {
        return text.Replace("\r", "");
    }
}
