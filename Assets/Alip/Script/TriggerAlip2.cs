using UnityEngine;

public class TriggerAlip2 : MonoBehaviour
{
    [SerializeField] private ClimbingRope ropeScript;
    [SerializeField] private SwitchCharacter switchScript;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        bool anyDisabled = false;

        if (ropeScript != null && ropeScript.enabled)
        {
            ropeScript.enabled = false;
            anyDisabled = true;
            Debug.Log("Rope script disabled.");
        }

        if (switchScript != null && switchScript.enabled)
        {
            switchScript.enabled = false;
            anyDisabled = true;
            Debug.Log("SwitchCharacter script disabled.");
        }
    }
}
