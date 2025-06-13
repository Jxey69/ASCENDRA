using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var respawn = other.GetComponent<PlayerRespawn>();
        if (respawn != null)
        {
            Debug.Log("Player entered death zone. Respawning...");
            respawn.Respawn();
        }
        else
        {
            Debug.LogWarning("PlayerRespawn component not found on player.");
        }
    }
}
