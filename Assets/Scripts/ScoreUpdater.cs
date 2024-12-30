using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
    
    GameManager gameManager;
    public TextMeshProUGUI player1Text;
    public TextMeshProUGUI player2Text;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            print("Game manager is null");
        }
        int player1ScoreInt = gameManager.printPlayer1Score();
        int player2ScoreInt = gameManager.printPlayer2Score();

        string player1Score = player1ScoreInt.ToString();
        string player2Score = player2ScoreInt.ToString();

        player1Text.text = player1Score;
        player2Text.text = player2Score;
        
    }


}
