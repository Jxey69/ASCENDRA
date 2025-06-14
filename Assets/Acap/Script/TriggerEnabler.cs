using UnityEngine;

public class TriggerEnabler : MonoBehaviour
{
    [SerializeField] private ThrowTrigger targetScript;
    [SerializeField] private FairyFollower fairy;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (targetScript != null && !targetScript.enabled)
        {
            targetScript.enabled = true;
            Debug.Log("Enabled Throw script.");
            fairy.EnableHelp();
            Debug.Log("Fairy can help.");

            var notifier = FindAnyObjectByType<UIThrowNotifier>();
            notifier?.ShowTempActivated();
        }


        Destroy(gameObject);
    }
}
