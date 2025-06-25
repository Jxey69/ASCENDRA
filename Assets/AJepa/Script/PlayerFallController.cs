using UnityEngine;
using System;

public class PlayerFallController : MonoBehaviour
{
    [SerializeField] private float fallThreshold = 74f;
    [SerializeField] private float slowFallGravity = 2f;

    private Rigidbody rb;
    private bool isSlowFalling = false;
    private bool triggeredFallEvent = false;
    private bool canTriggerFallSlow = false;

    public event Action OnFallBelowThreshold;
    public float FallThreshold => fallThreshold;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!canTriggerFallSlow) return;

        if (!triggeredFallEvent && transform.position.y < fallThreshold)
        {
            EnableSlowFall(true);
            triggeredFallEvent = true;
            OnFallBelowThreshold?.Invoke();
        }
    }

    public void EnableSlowFall(bool enable)
    {
        isSlowFalling = enable;
        rb.useGravity = !enable;
    }

    public void ResetFallState()
    {
        EnableSlowFall(false);
        triggeredFallEvent = false;
    }

    public void AllowSlowFall(bool allow)
    {
        canTriggerFallSlow = allow;
    }
}
