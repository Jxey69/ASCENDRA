using TMPro;
using UnityEngine;
using System.Collections;

public class UIRopeNotifier : MonoBehaviour
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

        tempText.text = "Press E when near the Red anchor ball \nto swing";
        yield return new WaitForSeconds(3f);

        tempText.text = "Press E again to let go";
        yield return new WaitForSeconds(3f);

        tempText.text = "You can combo with the teleportation ball \nto get closer to the anchor";
        yield return new WaitForSeconds(3f);

        tempText.text = "When falls, Fairy will try to help you \nby making a platform for you to stand for 10 seconds";
        yield return new WaitForSeconds(3f);

        tempText.text = "Use the time to throw your Kunai and get back.";
        yield return new WaitForSeconds(2f);

        tempText.gameObject.SetActive(false);
    }
}