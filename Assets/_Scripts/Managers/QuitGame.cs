using UnityEngine;

public class QuitGameManager : MonoBehaviour
{
    public void QuitGame()
    {
        // In the editor, this won't quit the game, but it works in a built version.
        Application.Quit();
        Debug.Log("Quit Game called");
    }
}