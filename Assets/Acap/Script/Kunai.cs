using UnityEngine;

public class Kunai : MonoBehaviour
{
    private Rigidbody rb;
    private bool stuck = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!stuck)
        {
            rb.isKinematic = true; // Stop moving and stick
            stuck = true;
        }
    }
}

