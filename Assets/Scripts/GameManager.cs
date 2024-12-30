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
    public GameObject player1DiedText;
    public GameObject player2DiedText;
    public GameObject player1WonText;
    public GameObject player2WonText;

    public static bool isGameOver = false;

    public GameObject planet1;
    public GameObject planet2;
    [SerializeField] private Transform transform1;
    [SerializeField] private Transform transform2;

    [SerializeField] int x1 = -100;
    [SerializeField] int x2 = 100;
    [SerializeField] int y1 = -70;
    [SerializeField] int y2 = 70;

    [SerializeField] float sceneLoadDelay = 1f;

    public GameObject gameWorld;

    public int printPlayer1Score()
    {
        return player1Wins;
    }

    public int printPlayer2Score() {  
        return player2Wins; 
    }
 
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
            player1WonText.SetActive(true);
            gameWorld.SetActive(false);
            ResetGame();
        }
        else if (player2Wins == roundsToWin)
        {
            player2WonText.SetActive(true);
            isGameOver = true;
            Debug.Log("Player 2 Wins the Game!");
            gameWorld.SetActive(false) ;
            ResetGame();
        }
        
    }

    public void PlayerDied(int loser)
    {

        if (isGameOver) return;

        if (loser == 1)
        {
            player1DiedText.SetActive(true);
            player2Wins++;
            lastRoundLoser = 1;
            round++;

        }
        else if (loser == 2)
        {
            player2DiedText.SetActive(true);
            player1Wins++;
            lastRoundLoser = 2;
            round++;
          
        }
        if (player1Wins == roundsToWin || player2Wins == roundsToWin)
        {
            isGameOver = true; // Mark the game as over
            Debug.Log("Game Over condition met!");
            ResetGame(); // Transition to Game Over scene
            LoadGameOverScene();
        }
        else
        {
            StartCoroutine(LoadSceneWithDelay(SceneManager.GetActiveScene().name, sceneLoadDelay));
            // Reload the current scene for the next round
            

        }

    }

    private IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
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
        //round = 1;
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(LoadSceneWithDelay("GameOverScene", sceneLoadDelay));
    }

    


}

