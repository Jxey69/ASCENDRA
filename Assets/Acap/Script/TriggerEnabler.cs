using UnityEngine;

public class TriggerEnabler : MonoBehaviour
{
    [SerializeField] private ThrowTrigger targetScript;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (targetScript != null && !targetScript.enabled)
        {
            targetScript.enabled = true;
            Debug.Log("Enabled Throw script.");

            var notifier = FindAnyObjectByType<UIThrowNotifier>();
            notifier?.ShowTempActivated();
        }

        Destroy(gameObject);
    }
}
