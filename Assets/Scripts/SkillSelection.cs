using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelection : MonoBehaviour
{
    public GameObject skillPanel; // Reference to the skill selection panel
    public Button skill1Button;
    public Button skill2Button;

    void Start()
    {
        // Show the skill panel only if a player needs to pick a skill
        if (GameManager.lastRoundLoser > 0 || (GameManager.player1Wins == 0 && GameManager.player2Wins == 0))
        {
            skillPanel.SetActive(true);
        }
        else
        {
            skillPanel.SetActive(false);
        }

        // Assign buttons dynamically for skills
        skill1Button.onClick.AddListener(() => ChooseSkill("Fireball"));
        skill2Button.onClick.AddListener(() => ChooseSkill("Shield"));
    }

    void ChooseSkill(string skill)
    {
        if (GameManager.lastRoundLoser == 1 || (GameManager.player1Wins == 0 && GameManager.player2Wins == 0))
        {
            GameManager.player1Skill = skill;
            Debug.Log($"Player 1 chose: {skill}");
        }
        else if (GameManager.lastRoundLoser == 2)
        {
            GameManager.player2Skill = skill;
            Debug.Log($"Player 2 chose: {skill}");
        }

        skillPanel.SetActive(false); // Hide the skill panel after selection
    }
}
