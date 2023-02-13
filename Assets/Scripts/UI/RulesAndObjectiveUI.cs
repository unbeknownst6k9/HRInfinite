using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RulesAndObjectiveUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI targetHired;

    [SerializeField]
    private TextMeshProUGUI UniversityBlackList;

    [SerializeField]
    private TextMeshProUGUI minAgeText;

    [SerializeField]
    private TextMeshProUGUI maxAgeText;

    [SerializeField]
    private GameObject ArtUIField;

    [SerializeField]
    private GameObject TechUIField;

    [SerializeField]
    private GameObject MarketingUIField;

    [SerializeField]
    private GameObject ManagementUIField;


    private void Start()
    {
        GameManager.Instance.OnResumeAccepted += updateUI;
        GameManager.Instance.OnNextRoundCallBack += roundUpdateUI;
        //updateUI();
        //roundUpdateUI();
        GameManager.Instance.OnDayStartCallBack += gameStartSetup;
    }

    private void gameStartSetup()
    {
        updateUI();
        roundUpdateUI();
    }


    public void updateUI()
    {
        targetHired.text = GameManager.Instance.getObjective().peopleHired.ToString() + " / " + GameManager.Instance.getObjective().peopleToHire.ToString();
    }

    public void roundUpdateUI()
    {
        updateUI();
        UniversityBlackList.text = convertToSingleString(GameManager.Instance.getObjective().BLUniversity);
        minAgeText.text = GameManager.Instance.getObjective().ageMin.ToString();
        maxAgeText.text = GameManager.Instance.getObjective().ageMax.ToString();
        updateSkillAreaUI();
    }

    private void updateSkillAreaUI()
    {
        ArtUIField.SetActive(false);
        TechUIField.SetActive(false);
        MarketingUIField.SetActive(false);
        ManagementUIField.SetActive(false);
        try
        {
            var objectiveField = GameManager.Instance.getObjective().skillRequirement;
            for (int i = 0; i < objectiveField.Length; i++)
            {
                switch (objectiveField[i])
                {
                    case DataStruct.skillArea.Art:
                        ArtUIField.SetActive(true);
                        break;
                    case DataStruct.skillArea.Tech:
                        TechUIField.SetActive(true);
                        break;
                    case DataStruct.skillArea.Marketing:
                        MarketingUIField.SetActive(true);
                        break;
                    case DataStruct.skillArea.Management:
                        ManagementUIField.SetActive(true);
                        break;
                    default:
                        Debug.Log("Skill area out of range");
                        break;
                }
            }

        }
        catch
        {
            Debug.LogWarning("Skill no exist");
        }
    }

    private string removeR(string text)
    {
        return text.Replace("\r", "");
    }

    private string convertToSingleString(string[] stringArray)
    {
        var newString = "";
        for (int i = 0; i < stringArray.Length; i++)
        {
            newString += removeR(stringArray[i]) + "\n";
        }
        return newString;
    }
}
