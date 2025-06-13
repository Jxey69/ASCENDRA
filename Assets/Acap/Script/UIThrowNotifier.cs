using TMPro;
using UnityEngine;
using System.Collections;

public class UIThrowNotifier : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tempText;
    [SerializeField] private float displayDuration = 2f;

    public void ShowTempActivated()
    {
        StopAllCoroutines();
        StartCoroutine(ShowTextCoroutine());
    }

    private IEnumerator ShowTextCoroutine()
    {
        tempText.gameObject.SetActive(true);
        tempText.text = "You can now Left Click to throw ender pearl";
        yield return new WaitForSeconds(displayDuration);
        tempText.gameObject.SetActive(false);
    }
}
