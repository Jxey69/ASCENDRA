using UnityEngine;

public class Tramp : MonoBehaviour
{
    private Rigidbody rb;
    public float bounceMultiplier = 1.5f; //Controls the fall speed on the bounce
    public float minimumBounceForce = 5f; //Minimum bounce even fall speed is low

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tramp"))
        {
            Debug.Log("Bounce!");

            float fallSpeed = Mathf.Abs(rb.linearVelocity.y);
            float bounceForce = Mathf.Max(fallSpeed * bounceMultiplier, minimumBounceForce);

            //Reset vertical velocity before applying new force for clean bounce
            Vector3 newVelocity = rb.linearVelocity;
            newVelocity.y = 0f;
            rb.linearVelocity = newVelocity;

            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
    }
}
