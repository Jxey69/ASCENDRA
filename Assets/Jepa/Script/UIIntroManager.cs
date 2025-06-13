using TMPro;
using UnityEngine;
using System.Collections;

public class UIIntroManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI introText;
    [SerializeField] private TextMeshProUGUI goalText;

    private void Start()
    {
        goalText.gameObject.SetActive(false);
        StartCoroutine(ShowIntroSequence());
    }

    private IEnumerator ShowIntroSequence()
    {
        introText.text = "WASD to move \nShift to run \nSpace to jump";
        yield return new WaitForSeconds(3f); 

        introText.text = "You can control your movement in the air";
        yield return new WaitForSeconds(3f);

        introText.text = "Every Parking Meter is a checkpoint \nGo near them to save";
        yield return new WaitForSeconds(3.5f);

        introText.text = "THE FLOOR IS LAVA";
        yield return new WaitForSeconds(4f);

        introText.gameObject.SetActive(false);
        goalText.text = "Reach the top";
        goalText.gameObject.SetActive(true);
    }
}
