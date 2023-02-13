using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenUI : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private float iconRotateSpeed;

    private RectTransform iconTransform;


    private void OnEnable()
    {
        if(iconTransform == null)
        {
            iconTransform = image.rectTransform;
        }
        StartCoroutine(spinIcon());
    }

    private IEnumerator spinIcon()
    {
        while (true)
        {
            iconTransform.Rotate(new Vector3(0, 0, iconRotateSpeed * Time.deltaTime *-1));
            yield return null;
        }
    }
}
