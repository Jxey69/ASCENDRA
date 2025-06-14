using UnityEngine;

public class Tramp : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;

    public float bounceMultiplier = 1.5f;
    public float minimumBounceForce = 5f;
    public AudioClip bounceSound; // assign this in the inspector

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tramp"))
        {
            Debug.Log("Bounce!");

            float fallSpeed = Mathf.Abs(rb.linearVelocity.y);
            float bounceForce = Mathf.Max(fallSpeed * bounceMultiplier, minimumBounceForce);

            // Reset vertical velocity before applying new force
            Vector3 newVelocity = rb.linearVelocity;
            newVelocity.y = 0f;
            rb.linearVelocity = newVelocity;

            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

            // Play bounce sound
            if (bounceSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(bounceSound);
            }
        }
    }
}

