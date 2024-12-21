using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSelection : MonoBehaviour
{
    public GameObject skillPanel; // Reference to the skill selection panel
    public GameObject firstRoundSkillPanel;

    //other rounds
    public Button skill1Button;
    public Button skill2Button;
    public Button skill3Button;

    //first round player 1
    public Button skill1FirstRoundButtonPlayer1;
    public Button skill2FirstRoundButtonPlayer1;
    public Button skill3FirstRoundButtonPlayer1;

    //first round player 2
    public Button skill1FirstRoundButtonPlayer2;
    public Button skill2FirstRoundButtonPlayer2;
    public Button skill3FirstRoundButtonPlayer2;
    
    //for the first round
    bool player1Selected = false;
    bool player2Selected = false;

    bool skillChosen = false;

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
        if ( (GameManager.player1Wins == 0 && GameManager.player2Wins == 0))
        {
            firstRoundSkillPanel.SetActive(true);
            ChooseRandomSkills(true); // Select random skills and update buttons
        }
        else if(GameManager.lastRoundLoser> 0)
        {
            print("Skill panel activated.");
            skillPanel.SetActive(true);
            ChooseRandomSkills(false);
        }
       
    }

    void roundOneChooseSkill(string skill, int player)
    {
        if (player == 1 && !player1Selected)
        {
            GameManager.player1Skills.Add(skill); // Add skill to Player 1's list
            Debug.Log($"Player 1 chose: {skill}");
            player1Selected = true;

        }
        else if (player == 2 && !player2Selected)
        {
            GameManager.player2Skills.Add(skill); // Add skill to Player 2's list
            Debug.Log($"Player 2 chose: {skill}");
            player2Selected = true;
        }

        if (player1Selected && player2Selected)
        {
            firstRoundSkillPanel.SetActive(false);
        }
    }

    void ChooseSkill(string skill, int player)
    {
        if (player == 1 && !skillChosen)
        {
            GameManager.player1Skills.Add(skill); // Add skill to Player 1's list
            Debug.Log($"Player 1 chose: {skill}");
            skillChosen = true;

        }
        else if (player == 2 && !skillChosen)
        {
            GameManager.player2Skills.Add(skill); // Add skill to Player 2's list
            Debug.Log($"Player 2 chose: {skill}");
            skillChosen = true;
     
        }

        if(skillChosen) {
            skillPanel.SetActive(false);
        }
        

    }

    private void ChooseRandomSkills(bool firstRound)
    {
        currentSkills.Clear();

        HashSet<int> selectedIndexes = new HashSet<int>();
        System.Random random = new System.Random();

        if (firstRound == true)
        {
            skill1FirstRoundButtonPlayer1.onClick.RemoveAllListeners();
            skill2FirstRoundButtonPlayer1.onClick.RemoveAllListeners();
            skill3FirstRoundButtonPlayer1.onClick.RemoveAllListeners();

            skill1FirstRoundButtonPlayer2.onClick.RemoveAllListeners();
            skill2FirstRoundButtonPlayer2.onClick.RemoveAllListeners();
            skill3FirstRoundButtonPlayer2.onClick.RemoveAllListeners();

            while (selectedIndexes.Count < 6)
            {
                int index = random.Next(allSkills.Count);
                selectedIndexes.Add(index);
            }

            foreach (int index in selectedIndexes)
            {
                currentSkills.Add(allSkills[index]);
            }

            skill1FirstRoundButtonPlayer1.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[0];
            skill2FirstRoundButtonPlayer1.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[1];
            skill3FirstRoundButtonPlayer1.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[2];

            skill1FirstRoundButtonPlayer2.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[3];
            skill2FirstRoundButtonPlayer2.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[4];
            skill3FirstRoundButtonPlayer2.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[5];

            skill1FirstRoundButtonPlayer1.onClick.AddListener(() => roundOneChooseSkill(currentSkills[0], 1));
            skill2FirstRoundButtonPlayer1.onClick.AddListener(() => roundOneChooseSkill(currentSkills[1], 1));
            skill3FirstRoundButtonPlayer1.onClick.AddListener(() => roundOneChooseSkill(currentSkills[2], 1));


            skill1FirstRoundButtonPlayer2.onClick.AddListener(() => roundOneChooseSkill(currentSkills[3], 2));
            skill2FirstRoundButtonPlayer2.onClick.AddListener(() => roundOneChooseSkill(currentSkills[4], 2));
            skill3FirstRoundButtonPlayer2.onClick.AddListener(() => roundOneChooseSkill(currentSkills[5], 2));


        }

        else
        {
            print("Choosing skills for other rounds");
            skill1Button.onClick.RemoveAllListeners();
            skill2Button.onClick.RemoveAllListeners();
            skill3Button.onClick.RemoveAllListeners();

            while (selectedIndexes.Count < 3)
            {
                int index = random.Next(allSkills.Count);
                selectedIndexes.Add(index);
            }

            foreach (int index in selectedIndexes)
            {
                currentSkills.Add(allSkills[index]);
            }

            skill1Button.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[0];
            skill2Button.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[1];
            skill3Button.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[2];

            if (GameManager.lastRoundLoser == 1)
            {
                skill1Button.onClick.AddListener(() => ChooseSkill(currentSkills[0], 1));
                skill2Button.onClick.AddListener(() => ChooseSkill(currentSkills[1], 1));
                skill3Button.onClick.AddListener(() => ChooseSkill(currentSkills[2], 1));
            }
            else if (GameManager.lastRoundLoser == 2)
            {
                skill1Button.onClick.AddListener(() => ChooseSkill(currentSkills[0], 2));
                skill2Button.onClick.AddListener(() => ChooseSkill(currentSkills[1], 2));
                skill3Button.onClick.AddListener(() => ChooseSkill(currentSkills[2], 2));
            }

           

        }              
    }
}
