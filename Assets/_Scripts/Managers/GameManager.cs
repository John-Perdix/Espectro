using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;

    public void PauseGame()
    {
        isPaused = !isPaused; // Toggle the pause state

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }
    }

    // Optional: A separate function for a dedicated "Resume" button
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        // In the editor, this won't quit the game, but it works in a built version.
        Application.Quit();
        Debug.Log("Quit Game called");
    }
}