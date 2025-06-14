using UnityEngine;
using System.Collections;

public class FairyFollower : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(1f, 2f, 0f);
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private float fallThreshold = -10f;
    [SerializeField] private float platformDuration = 10f;
    [SerializeField] private float circlingDuration = 2f;
    [SerializeField] private float circlingRadius = 2f;
    [SerializeField] private float circlingSpeed = 180f;
    [SerializeField] private Vector3 platformOffset = new Vector3(0f, -1f, 0f);

    private GameObject currentPlatform;
    private bool isCircling = false;
    private bool canHelpPlayer = false;
    private float circlingTimer = 0f;
    private Vector3 platformCenter;

    public void EnableHelp()
    {
        canHelpPlayer = true;
    }

    private void Update()
    {
        if (isCircling)
        {
            CircleAroundPlatform();
        }
        else
        {
            FollowPlayer();
            TrySpawnPlatform();
        }

        LookAtPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    private void TrySpawnPlatform()
    {
        if (!canHelpPlayer || currentPlatform != null) return;

        if (player.position.y < fallThreshold)
        {
            platformCenter = player.position + platformOffset;
            currentPlatform = Instantiate(platformPrefab, platformCenter, Quaternion.identity);
            StartCoroutine(CircleAndThenResume());
            StartCoroutine(RemovePlatformAfterDelay());
        }
    }

    private IEnumerator CircleAndThenResume()
    {
        isCircling = true;
        circlingTimer = 0f;

        while (circlingTimer < circlingDuration)
        {
            circlingTimer += Time.deltaTime;
            yield return null;
        }

        isCircling = false;
    }

    private void CircleAroundPlatform()
    {
        float angle = circlingSpeed * Time.time;
        float rad = angle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), 0.5f, Mathf.Sin(rad)) * circlingRadius;
        transform.position = platformCenter + offset;
    }

    private void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
    }

    private IEnumerator RemovePlatformAfterDelay()
    {
        yield return new WaitForSeconds(platformDuration);
        if (currentPlatform != null)
        {
            Destroy(currentPlatform);
            currentPlatform = null;
        }
    }
}
