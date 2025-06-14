using UnityEngine;
using TMPro;

public class HeightTracker : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI heightText;

    private float baseHeight;
    private float maxClimbed = 0f;
    private float displayedHeight = 0f;  // For smooth transition

    void Start()
    {
        baseHeight = player.position.y;
    }

    void Update()
    {
        float climbed = player.position.y - baseHeight;

        if (climbed > maxClimbed)
            maxClimbed = climbed;

        // Smoothly animate the height display
        displayedHeight = Mathf.Lerp(displayedHeight, maxClimbed, Time.deltaTime * 5f);

        heightText.text = string.Format("Highest Point: {0:n1}m", displayedHeight);
    }
}
