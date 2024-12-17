using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSelection : MonoBehaviour
{
    public GameObject skillPanel; // Reference to the skill selection panel
    public Button skill1Button;
    public Button skill2Button;
    public Button skill3Button;

    private List<string> allSkills = new List<string>
    {
        "Fireball", "Shield", "Teleport", "Dash",
        "Freeze", "Leap", "Punch", "Flame",
        "IceBlast", "SuperKick"
    };

    private List<string> currentSkills = new List<string>();

    void Start()
    {
        // Show the skill panel only if a player needs to pick a skill
        if (GameManager.lastRoundLoser > 0 || (GameManager.player1Wins == 0 && GameManager.player2Wins == 0))
        {
            skillPanel.SetActive(true);
            ChooseRandomSkills(); // Select random skills and update buttons
        }
        else
        {
            skillPanel.SetActive(false);
        }
    }

    void ChooseSkill(string skill)
    {
        // Assign the chosen skill to the appropriate player
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

    private void ChooseRandomSkills()
    {
        // Clear previous skills and listeners
        currentSkills.Clear();
        skill1Button.onClick.RemoveAllListeners();
        skill2Button.onClick.RemoveAllListeners();
        skill3Button.onClick.RemoveAllListeners();

        HashSet<int> selectedIndexes = new HashSet<int>();
        System.Random random = new System.Random();

        // Select 3 unique random skills
        while (selectedIndexes.Count < 3)
        {
            int index = random.Next(allSkills.Count);
            selectedIndexes.Add(index);
        }

        // Fill the currentSkills list
        foreach (int index in selectedIndexes)
        {
            currentSkills.Add(allSkills[index]);
        }

        // Update button texts with the selected skills
        skill1Button.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[0];
        skill2Button.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[1];
        skill3Button.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[2];

        // Add listeners to the buttons dynamically
        skill1Button.onClick.AddListener(() => ChooseSkill(currentSkills[0]));
        skill2Button.onClick.AddListener(() => ChooseSkill(currentSkills[1]));
        skill3Button.onClick.AddListener(() => ChooseSkill(currentSkills[2]));
    }
}
