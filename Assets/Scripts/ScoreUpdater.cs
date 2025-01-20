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
        int player1ScoreInt = GameManager.printPlayer1Score();
        int player2ScoreInt = GameManager.printPlayer2Score();

        print(player1ScoreInt + " " + player2ScoreInt); 

        string player1Score = player1ScoreInt.ToString();
        string player2Score = player2ScoreInt.ToString();

        player1Text.text = "Player 1 Score:" + "\n" + player1Score;
        player2Text.text = "Player 2 Score:" + "\n" + player2Score;
        
    }


}
