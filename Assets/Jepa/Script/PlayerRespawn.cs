using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Respawn()
    {
        transform.position = CheckpointManager.Instance.GetCheckpoint();
        rb.linearVelocity = Vector3.zero;
    }
}
