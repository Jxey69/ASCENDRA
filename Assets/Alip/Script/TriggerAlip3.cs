using UnityEngine;

public class TriggerAlip3 : MonoBehaviour
{
    [SerializeField] private ClimbingRope2 ropeScript;
    [SerializeField] private SwitchCharacter switchCharacterScript;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        bool anyEnabled = false;

        if (ropeScript != null && !ropeScript.enabled)
        {
            ropeScript.enabled = true;
            Debug.Log("Enabled Rope script.");
            anyEnabled = true;
        }

        if (switchCharacterScript != null && !switchCharacterScript.enabled)
        {
            switchCharacterScript.enabled = true;
            Debug.Log("Enabled SwitchCharacter script.");
            anyEnabled = true;
        }
    }
}

