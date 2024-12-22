using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int player1Wins = 0;
    public static int player2Wins = 0;
    public static int roundsToWin = 2;

    public static List<string> player1Skills = new List<string>(); // Store multiple skills
    public static List<string> player2Skills = new List<string>();

    public static int lastRoundLoser = 0;

    public static int round = 1;

    public static string player1SkillsString = "";
    public static string player2SkillsString = "";

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }




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
            }
            else if (lastRoundLoser == 1)
            {
                Debug.Log("Player 1 lost the previous round. Player 1 chooses a skill.");
            }
            else if (lastRoundLoser == 2)
            {
                Debug.Log("Player 2 lost the previous round. Player 2 chooses a skill.");
            }
        }
    }

    public void PlayerDied(int loser)
    {
        if (loser == 1)
        {
            player2Wins++;
            lastRoundLoser = 1;
        }
        else if (loser == 2)
        {
            player1Wins++;
            lastRoundLoser = 2;
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
        
        lastRoundLoser = 0;

        // Load Title Screen or End Game Scene
        SceneManager.LoadScene("GameOverScene");
        //player1Skills.Clear();
        //player2Skills.Clear();
    }

    public string printPlayer1Skills() { 
        int player1SkillNumber = player1Skills.Count;
        

        
        for(int i = 0; i < player1SkillNumber; i++)
        {
            player1SkillsString = player1SkillsString + "\n" + player1Skills[i];
        }
        print("Player 1 skills are:" + player1SkillsString);
        return player1SkillsString;
        
        
    }

    public string printPlayer2Sills()
    {
        int player2SkillNumber = player2Skills.Count;

        
        for (int i = 0; i < player2SkillNumber; i++)
        {
            player2SkillsString = player2SkillsString + "\n" + player2Skills[i];
        }
        print("Player 2 skills are:" + player2SkillsString);
        return player2SkillsString;
    }



}

