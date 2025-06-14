using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public GameObject playerLocomotion, playerSwing;

    public AudioClip switchSound;
    private AudioSource audioSource;

    public bool isSwing = false;

    void Start()
    {
        playerLocomotion.SetActive(true);
        playerSwing.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (switchSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(switchSound);
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
