using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject player;

    private bool isGameOver = false;

    private void Start()
    {
        // Disable the Game Over panel at the start of the game.
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        // Check for game over conditions (e.g., falling down the void) and call GameOver() when needed.
        if (isGameOver)
        {
            return; // Don't execute the rest of the Update loop once the game is over.
        }

        // Check if the player has fallen down the void. You can implement your own condition here.
        if (PlayerHasFallenDownTheVoid())
        {
            Debug.Log("Game Over");
            GameOver();
        }
    }

    private bool PlayerHasFallenDownTheVoid()
    {
        
        float yThreshold = -10f; // Adjust this value as needed.
        float playerY = transform.position.y;
        Debug.Log("Player Y Position: " + playerY);
        // Check if the player's Y position is below the threshold.
        if (playerY < yThreshold)
        {
            return true; // Player has fallen down the void.
        }

        return false;
    }

    private void GameOver()
    {
        // Set the game over flag to true.
        isGameOver = true;

        // Show the Game Over panel.
        gameOverPanel.SetActive(true);

        // You can also perform other game over actions here, such as pausing the game.
    }

    public void RestartGame()
    {
        // Reload the current scene (assuming your game has only one scene).
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
