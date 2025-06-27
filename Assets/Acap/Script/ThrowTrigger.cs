using UnityEngine;

public class ThrowTrigger : MonoBehaviour
{
    [Header("References")]
    public GameObject throwItem;            // Kunai prefab with TeleportAnchor
    public Transform throwPoint;            // Throw origin point
    public Camera playerCamera;             // Aiming camera

    [Header("Throw Settings")]
    public float minThrowForce = 500f;
    public float maxThrowForce = 2000f;
    public float maxChargeTime = 2f;
    public float teleportYOffset = 1f;

    [Header("Audio")]
    [SerializeField] private AudioClip throwSound;
    [SerializeField] private AudioSource audioSource;

    private GameObject tpItem;
    private TeleportAnchor tpAnchor;
    private float chargeTime;
    private bool isCharging;

    void Update()
    {
        HandleThrow();
        HandleTeleport();
        HandleReset();
    }

    private void HandleThrow()
    {
        if (Input.GetButtonDown("Fire1") && tpItem == null)
        {
            chargeTime = 0f;
            isCharging = true;
        }

        if (Input.GetButton("Fire1") && isCharging)
        {
            chargeTime += Time.deltaTime;
        }

        if (Input.GetButtonUp("Fire1") && isCharging)
        {
            float t = Mathf.Clamp01(chargeTime / maxChargeTime);
            float throwForce = Mathf.Lerp(minThrowForce, maxThrowForce, t);

            tpItem = Instantiate(throwItem, throwPoint.position, Quaternion.identity);
            tpAnchor = tpItem.GetComponent<TeleportAnchor>();

            if (tpItem.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.sleepThreshold = 0f;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                Vector3 dir = playerCamera.transform.forward;
                rb.AddForce(dir * throwForce, ForceMode.Force);
            }

            if (audioSource != null && throwSound != null)
            {
                audioSource.PlayOneShot(throwSound);
            }

            isCharging = false;
        }
    }

   private void HandleTeleport()
{
    if (Input.GetKeyDown(KeyCode.T) && tpItem != null && tpAnchor != null)
    {
        Vector3 tpPos = tpAnchor.CurrentPosition + Vector3.up * teleportYOffset;

        Debug.Log($"Trying to teleport to: {tpPos}");

        if (!Physics.CheckSphere(tpPos, 0.2f))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero; // stop current motion
                rb.MovePosition(tpPos);     // teleport via physics
            }
            else
            {
                transform.position = tpPos; // fallback if no RB
            }

            Debug.Log("Teleport success!");
            Destroy(tpItem);
            tpItem = null;
            tpAnchor = null;
        }
        else
        {
            Debug.LogWarning("Teleport blocked: space occupied.");
        }
    }
}


    private void HandleReset()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (tpItem != null)
                Destroy(tpItem);
            tpItem = null;
            tpAnchor = null;
        }
    }
}
