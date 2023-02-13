using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]
    private GameObject pageContainer;

    [SerializeField]
    private TextMeshProUGUI pageNumber;

    private int index;

    private int totalPages;
    // Start is called before the first frame update
    void Start()
    {
        pageContainer.transform.GetChild(index).gameObject.SetActive(true);
        totalPages = pageContainer.transform.childCount;
        pageNumber.text = (index + 1).ToString() + " / " + totalPages.ToString();
    }


    private void UpdatePage(int newPage)
    {
        if (newPage >= 0 && newPage < totalPages)
        {
            pageContainer.transform.GetChild(index).gameObject.SetActive(false);
            index = newPage;
            pageContainer.transform.GetChild(index).gameObject.SetActive(true);
            pageNumber.text = (index + 1).ToString() + " / " + totalPages.ToString();
        }
    }

    public void nextPage()
    {
        UpdatePage(index + 1);
    }

    public void previousPage()
    {
        UpdatePage(index - 1);
    }

    public void backButton()
    {
        pageContainer.transform.GetChild(index).gameObject.SetActive(false);
        index = 0;
        pageContainer.transform.GetChild(index).gameObject.SetActive(true);
        pageNumber.text = (index + 1).ToString() + " / " + totalPages.ToString();
        gameObject.SetActive(false);
    }
}
