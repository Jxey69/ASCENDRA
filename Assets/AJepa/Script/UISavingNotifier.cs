using TMPro;
using UnityEngine;
using System.Collections;

public class UISavingNotifier : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI savingText;
    [SerializeField] private float displayDuration = 2f;

    public void ShowSavingText()
    {
        StopAllCoroutines(); // in case player triggers multiple quickly
        StartCoroutine(ShowTextCoroutine());
    }

    private IEnumerator ShowTextCoroutine()
    {
        savingText.gameObject.SetActive(true);
        savingText.text = "Saving...";
        yield return new WaitForSeconds(displayDuration);
        savingText.gameObject.SetActive(false);
    }
}
