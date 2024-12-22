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

        player1Text.text = gameManager.printPlayer1Skills();
        player2Text.text = gameManager.printPlayer2Sills();
    }


}
