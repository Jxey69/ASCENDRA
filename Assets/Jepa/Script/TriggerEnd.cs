using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerEnd : MonoBehaviour
{
    [SerializeField] private Canvas messageCanvas;
    [SerializeField] private TextMeshProUGUI messageTMP;
    [SerializeField] private float delayBeforeLoad = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        messageCanvas.gameObject.SetActive(true);
        messageTMP.text = "You have reached the top!\nThanks for playing.";

        StartCoroutine(LoadMainMenuAfterDelay());
    }

    private IEnumerator LoadMainMenuAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene("MainMenu");
    }
}
