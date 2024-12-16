using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int player1Wins = 0;
    public static int player2Wins = 0;
    public static int roundsToWin = 2;

    public static string player1Skill = ""; // Skills as strings for now
    public static string player2Skill = "";

    public static int lastRoundLoser = 0;

    public static int round = 1;



    void Start()
    {
        // Check if a player has won the game
        if (player1Wins == roundsToWin)
        {
            Debug.Log("Player 1 Wins the Game!");
            ResetGame();
        }
        else if (player2Wins == roundsToWin)
        {
            Debug.Log("Player 2 Wins the Game!");
            ResetGame();
        }
        else
        {
            // Determine skill selection at the start of each round
            if (player1Wins == 0 && player2Wins == 0)
            {
                Debug.Log("Round 1: Both players choose a skill.");
                // Trigger UI or logic for skill selection
            }
            else if (lastRoundLoser == 1)
            {
                Debug.Log("Player 1 lost the previous round. Player 1 chooses a skill.");
                // Trigger Player 1 skill selection UI
            }
            else if (lastRoundLoser == 2)
            {
                Debug.Log("Player 2 lost the previous round. Player 2 chooses a skill.");
                // Trigger Player 2 skill selection UI
            }
        }
    }

    public void PlayerDied(int winner)
    {
        if (winner == 1)
        {
            player1Wins++;
            lastRoundLoser = 2;
        }
        else if (winner == 2)
        {
            player2Wins++;
            lastRoundLoser = 1;
        }

        Debug.Log($"Player 1 Wins: {player1Wins}, Player 2 Wins: {player2Wins}");

        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetGame()
    {
        // Reset everything for a new game
        player1Wins = 0;
        player2Wins = 0;
        player1Skill = "";
        player2Skill = "";
        lastRoundLoser = 0;

        // Load Title Screen or End Game Scene
        SceneManager.LoadScene("GameOverScene");
    }
}
