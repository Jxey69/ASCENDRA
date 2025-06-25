using UnityEngine;

public class Tramp : MonoBehaviour
{
    [SerializeField] private float bounceMultiplier = 2f;
    [SerializeField] private float minimumBounceForce = 5f;
    [SerializeField] private float chargeMultiplier = 4f;
    [SerializeField] private float maxChargeTime = 2f;
    [SerializeField] private AudioClip bounceSound;
    [SerializeField] private AudioSource audioSource;

    private float chargeTime = 0f;
    private bool isCharging = false;
    private Rigidbody playerRb;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        playerRb = collision.rigidbody;
        isCharging = true;
        chargeTime = 0f;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!isCharging || !collision.gameObject.CompareTag("Player")) return;

        if (Input.GetKey(KeyCode.Space))
        {
            chargeTime += Time.fixedDeltaTime;
            chargeTime = Mathf.Min(chargeTime, maxChargeTime);

            Debug.Log($"Charging... ({chargeTime:F1} / {maxChargeTime})");
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") || playerRb == null) return;

        ApplyBounce();
    }

    private void Update()
    {
        // Release triggers bounce if on trampoline
        if (isCharging && Input.GetKeyUp(KeyCode.Space))
        {
            ApplyBounce();
        }
    }

    private void ApplyBounce()
    {
        float fallSpeed = Mathf.Abs(playerRb.linearVelocity.y);
        float bounceForce = Mathf.Max(fallSpeed * bounceMultiplier, minimumBounceForce);
        bounceForce += chargeTime * chargeMultiplier;

        Vector3 newVelocity = playerRb.linearVelocity;
        newVelocity.y = bounceForce;
        playerRb.linearVelocity = newVelocity;

        if (audioSource && bounceSound)
            audioSource.PlayOneShot(bounceSound);

        isCharging = false;
        chargeTime = 0f;
        playerRb = null;

        Debug.Log($"Bounce applied: {bounceForce}");
    }
}
