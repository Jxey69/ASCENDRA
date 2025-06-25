using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private string checkpointID;
    public string ID => checkpointID;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log($"Checkpoint triggered: {checkpointID} at position {transform.position}");

        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.SaveCheckpoint(transform.position);
            Debug.Log("Checkpoint position saved.");
        }
        else
        {
            Debug.LogError("CheckpointManager instance is null.");
        }
    }
}
