using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
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

    public TextMeshProUGUI scoreText;

    public static bool isGameOver = false;

    public GameObject planet1;
    public GameObject planet2;
    [SerializeField] private Transform transform1;
    [SerializeField] private Transform transform2;

    [SerializeField] int x1 = -100;
    [SerializeField] int x2 = 100;
    [SerializeField] int y1 = -70;
    [SerializeField] int y2 = 70;
 
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update scoreText UI
        if (scoreText != null)
        {
            scoreText.text = "Round " + round;
        }

        if (!isGameOver)
        {
            // Reassign planet references
            planet1 = GameObject.Find("Planet1Control");
            planet2 = GameObject.Find("Planet2Control");

            if (planet1 != null)
                transform1 = planet1.transform;
            else
                Debug.LogError("Planet1 not found!");

            if (planet2 != null)
                transform2 = planet2.transform;
            else
                Debug.LogError("Planet2 not found!");

            // Randomize positions
            RandomizePlanetPositions();
        }

        
    }




    void Start()
    {
        //Transform transform1 = planet1.GetComponent<Transform>();
        //Transform transform2 = planet2.GetComponent<Transform>();

        // Check if a player has won the game
        if (player1Wins == roundsToWin)
        {
            isGameOver = true;
            Debug.Log("Player 1 Wins the Game!");
            ResetGame();
        }
        else if (player2Wins == roundsToWin)
        {
            isGameOver = true;
            Debug.Log("Player 2 Wins the Game!");
            ResetGame();
        }
        
    }

    public void PlayerDied(int loser)
    {

        if (isGameOver) return;

        if (loser == 1)
        {
            player2Wins++;
            lastRoundLoser = 1;
            round++;
           
        }
        else if (loser == 2)
        {
            player1Wins++;
            lastRoundLoser = 2;
            round++;
          
        }
        if (player1Wins == roundsToWin || player2Wins == roundsToWin)
        {
            isGameOver = true; // Mark the game as over
            Debug.Log("Game Over condition met!");
            ResetGame(); // Transition to Game Over scene
        }
        else
        {
            // Reload the current scene for the next round
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }

    private void RandomizePlanetPositions()
    {
        int x11 = Random.Range(x1, x2);
        int y11 = Random.Range(y1, y2);
        int x22 = Random.Range(x1, x2);
        int y22 = Random.Range(y1, y2);
        UnityEngine.Vector3 planet1randomPosition = new UnityEngine.Vector3(x11, y11, 0);
        transform1.position = planet1randomPosition;
        


        UnityEngine.Vector3 planet2randomPosition = new UnityEngine.Vector3(x22, y22, 0);
        transform2.position = planet2randomPosition;


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
        //round = 1;
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

