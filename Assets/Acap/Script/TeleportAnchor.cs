using UnityEngine;

public class TeleportAnchor : MonoBehaviour
{
    public Vector3 CurrentPosition { get; private set; }

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb != null)
        {
            CurrentPosition = rb.position;
        }
        else
        {
            CurrentPosition = transform.position;
        }
    }
}
