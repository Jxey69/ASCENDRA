using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public GameObject playerLocomotion, playerSwing;
    public bool isSwing = false;
    [SerializeField] private AudioClip hookSound;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        playerLocomotion.SetActive(true);
        playerSwing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (audioSource != null && hookSound != null)
            {
                audioSource.PlayOneShot(hookSound);
            }

            isSwing = !isSwing;
            if (isSwing)
            {
                playerSwing.transform.position = playerLocomotion.transform.position;
                playerLocomotion.SetActive(false);
                playerSwing.SetActive(true);
            }
            else
            {
                playerLocomotion.transform.position = playerSwing.transform.position;
                playerLocomotion.SetActive(true);
                playerSwing.SetActive(false);
            }
        }
    }

}
