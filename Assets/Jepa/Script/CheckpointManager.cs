using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    private Vector3 _lastCheckpoint;
    private UISavingNotifier uiNotifier;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        uiNotifier = FindAnyObjectByType<UISavingNotifier>();
    }

    public void SaveCheckpoint(Vector3 position)
    {
        _lastCheckpoint = position;
        uiNotifier?.ShowSavingText();
    }

    public Vector3 GetCheckpoint()
    {
        return _lastCheckpoint;
    }
}
