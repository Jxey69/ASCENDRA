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
        tempText.text = "You can now Left Click to throw teleportation ball!";
        yield return new WaitForSeconds(3f);

        tempText.text = "Press T to Teleport \nPress Y to Cancel";
        yield return new WaitForSeconds(3f);

        tempText.text = "Hold T to charge your throw";
        yield return new WaitForSeconds(3f);
        tempText.gameObject.SetActive(false);
    }
}
