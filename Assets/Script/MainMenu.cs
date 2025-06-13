using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Call this from the Play button's OnClick event
    public void PlayGame()
    {
        // Replace "GameScene" with your actual gameplay scene name
        SceneManager.LoadScene("MainScene");
    }

    // Call this from the Quit button's OnClick event
    public void QuitGame()
    {
        // This will quit the application
        Application.Quit();

#if UNITY_EDITOR
        // If running in the editor, stop play mode
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}