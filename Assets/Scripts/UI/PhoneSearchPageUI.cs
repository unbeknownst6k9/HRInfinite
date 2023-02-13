using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhoneSearchPageUI : MonoBehaviour, UIInterface
{
    #region Fields
    [SerializeField]
    private TextMeshProUGUI Name;
    [SerializeField]
    private TextMeshProUGUI Age;
    [SerializeField]
    private TextMeshProUGUI Email;
    #endregion

    public void updateUI()
    {
        Name.text = removeR(GameManager.Instance.currentResume.realFirstName+" " + GameManager.Instance.currentResume.lastName);
        Age.text = removeR(GameManager.Instance.currentResume.realAge.ToString());
        Email.text = removeR(GameManager.Instance.currentResume.realEmail);
    }

    private string removeR(string text)
    {
        return text.Replace("\r", "");
    }

}
