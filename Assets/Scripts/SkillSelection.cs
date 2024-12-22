using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSelection : MonoBehaviour
{
    public GameObject skillPanel; // Reference to the skill selection panel
    public GameObject firstRoundSkillPanel;

    // Buttons for first round (Player 1 and Player 2)
    public Button skill1FirstRoundButtonPlayer1;
    public Button skill2FirstRoundButtonPlayer1;
    public Button skill3FirstRoundButtonPlayer1;

    public Button skill1FirstRoundButtonPlayer2;
    public Button skill2FirstRoundButtonPlayer2;
    public Button skill3FirstRoundButtonPlayer2;

    // Buttons for other rounds
    public Button skill1Button;
    public Button skill2Button;
    public Button skill3Button;

    private Button[] player1Buttons;
    private Button[] player2Buttons;
    private Button[] otherRoundButtons;

    private int currentSelectionIndexPlayer1 = 0;
    private int currentSelectionIndexPlayer2 = 0;
    private int currentSelectionIndexOtherRound = 0;

    private bool player1Selected = false;
    private bool player2Selected = false;
    private bool skillChosen = false;

    private List<string> allSkills = new List<string>
    {
        "Fireball", "Shield", "Teleport", "Dash",
        "Freeze", "Leap", "Punch", "Flame",
        "IceBlast", "SuperKick"
    };

    private List<string> currentSkills = new List<string>();

    void Start()
    {
        if (GameManager.player1Wins == 0 && GameManager.player2Wins == 0)
        {
            firstRoundSkillPanel.SetActive(true);
            player1Buttons = new[] { skill1FirstRoundButtonPlayer1, skill2FirstRoundButtonPlayer1, skill3FirstRoundButtonPlayer1 };
            player2Buttons = new[] { skill1FirstRoundButtonPlayer2, skill2FirstRoundButtonPlayer2, skill3FirstRoundButtonPlayer2 };
            ChooseRandomSkills(true);
            HighlightButton(player1Buttons, currentSelectionIndexPlayer1, true); // Highlight Player 1's first button
            HighlightButton(player2Buttons, currentSelectionIndexPlayer2, false); // Highlight Player 2's first button
        }
        else if (GameManager.lastRoundLoser > 0)
        {
            skillPanel.SetActive(true);
            otherRoundButtons = new[] { skill1Button, skill2Button, skill3Button };
            ChooseRandomSkills(false); // New skills for subsequent rounds
            HighlightButton(otherRoundButtons, currentSelectionIndexOtherRound, GameManager.lastRoundLoser == 1); // Highlight based on the last round loser
        }
    }

    void Update()
    {
        if (firstRoundSkillPanel.activeSelf)
        {
            HandlePlayer1Input(player1Buttons, ref currentSelectionIndexPlayer1, 1); // First round input for Player 1
            HandlePlayer2Input(player2Buttons, ref currentSelectionIndexPlayer2, 2); // First round input for Player 2
        }
        else if (skillPanel.activeSelf)
        {
            if (GameManager.lastRoundLoser == 1)
            {
                HandlePlayer1Input(otherRoundButtons, ref currentSelectionIndexOtherRound, 1); // Input for Player 1 in subsequent rounds
            }
            else if (GameManager.lastRoundLoser == 2)
            {
                HandlePlayer2Input(otherRoundButtons, ref currentSelectionIndexOtherRound, 2); // Input for Player 2 in subsequent rounds
            }
        }
    }

    private void HandlePlayer1Input(Button[] buttons, ref int currentIndex, int player)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentIndex = (currentIndex - 1 + buttons.Length) % buttons.Length; // Navigate up
            HighlightButton(buttons, currentIndex, true);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            currentIndex = (currentIndex + 1) % buttons.Length; // Navigate down
            HighlightButton(buttons, currentIndex, true);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            buttons[currentIndex].onClick.Invoke(); // Select the highlighted button
        }
    }

    private void HandlePlayer2Input(Button[] buttons, ref int currentIndex, int player)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = (currentIndex - 1 + buttons.Length) % buttons.Length; // Navigate up
            HighlightButton(buttons, currentIndex, false);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % buttons.Length; // Navigate down
            HighlightButton(buttons, currentIndex, false);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            buttons[currentIndex].onClick.Invoke(); // Select the highlighted button
        }
    }

    private void HighlightButton(Button[] buttons, int index, bool isPlayer1)
    {
        foreach (var button in buttons)
        {
            button.GetComponent<Image>().color = Color.white; // Reset button color
        }
        buttons[index].GetComponent<Image>().color = isPlayer1 ? Color.red : Color.blue; // Highlight for Player 1 or 2
    }

    void roundOneChooseSkill(string skill, int player)
    {
        if (player == 1 && !player1Selected)
        {
            GameManager.player1Skills.Add(skill); // Save Player 1's skill
            Debug.Log($"Player 1 chose: {skill}");
            player1Selected = true;
        }
        else if (player == 2 && !player2Selected)
        {
            GameManager.player2Skills.Add(skill); // Save Player 2's skill
            Debug.Log($"Player 2 chose: {skill}");
            player2Selected = true;
        }

        if (player1Selected && player2Selected)
        {
            firstRoundSkillPanel.SetActive(false); // Disable first round panel
        }
    }

    void ChooseSkill(string skill, int player)
    {
        if (player == 1 && !skillChosen)
        {
            GameManager.player1Skills.Add(skill); // Player 1 chooses skill in subsequent rounds
            Debug.Log($"Player 1 chose: {skill}");
            skillChosen = true;
        }
        else if (player == 2 && !skillChosen)
        {
            GameManager.player2Skills.Add(skill); // Player 2 chooses skill in subsequent rounds
            Debug.Log($"Player 2 chose: {skill}");
            skillChosen = true;
        }

        if (skillChosen)
        {
            skillPanel.SetActive(false); // Disable subsequent round panel
        }
    }

    private void ChooseRandomSkills(bool firstRound)
    {
        currentSkills.Clear();

        HashSet<int> selectedIndexes = new HashSet<int>();
        System.Random random = new System.Random();

        if (firstRound)
        {
            while (selectedIndexes.Count < 6)
            {
                int index = random.Next(allSkills.Count); // Select unique random skills
                selectedIndexes.Add(index);
            }

            foreach (int index in selectedIndexes)
            {
                currentSkills.Add(allSkills[index]); // Add to the current round skill list
            }

            // Assign skills to first round buttons
            skill1FirstRoundButtonPlayer1.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[0];
            skill2FirstRoundButtonPlayer1.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[1];
            skill3FirstRoundButtonPlayer1.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[2];

            skill1FirstRoundButtonPlayer2.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[3];
            skill2FirstRoundButtonPlayer2.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[4];
            skill3FirstRoundButtonPlayer2.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[5];

            // Add listeners for first round skills
            skill1FirstRoundButtonPlayer1.onClick.AddListener(() => roundOneChooseSkill(currentSkills[0], 1));
            skill2FirstRoundButtonPlayer1.onClick.AddListener(() => roundOneChooseSkill(currentSkills[1], 1));
            skill3FirstRoundButtonPlayer1.onClick.AddListener(() => roundOneChooseSkill(currentSkills[2], 1));

            skill1FirstRoundButtonPlayer2.onClick.AddListener(() => roundOneChooseSkill(currentSkills[3], 2));
            skill2FirstRoundButtonPlayer2.onClick.AddListener(() => roundOneChooseSkill(currentSkills[4], 2));
            skill3FirstRoundButtonPlayer2.onClick.AddListener(() => roundOneChooseSkill(currentSkills[5], 2));
        }
        else
        {
            while (selectedIndexes.Count < 3)
            {
                int index = random.Next(allSkills.Count); // Select 3 unique random skills
                selectedIndexes.Add(index);
            }

            foreach (int index in selectedIndexes)
            {
                currentSkills.Add(allSkills[index]); // Add to the current round skill list
            }

            // Assign skills to subsequent round buttons
            skill1Button.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[0];
            skill2Button.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[1];
            skill3Button.GetComponentInChildren<TextMeshProUGUI>().text = currentSkills[2];

            if (GameManager.lastRoundLoser == 1)
            {
                // Add listeners for Player 1's skill choice
                skill1Button.onClick.AddListener(() => ChooseSkill(currentSkills[0], 1));
                skill2Button.onClick.AddListener(() => ChooseSkill(currentSkills[1], 1));
                skill3Button.onClick.AddListener(() => ChooseSkill(currentSkills[2], 1));
            }
            else if (GameManager.lastRoundLoser == 2)
            {
                // Add listeners for Player 2's skill choice
                skill1Button.onClick.AddListener(() => ChooseSkill(currentSkills[0], 2));
                skill2Button.onClick.AddListener(() => ChooseSkill(currentSkills[1], 2));
                skill3Button.onClick.AddListener(() => ChooseSkill(currentSkills[2], 2));
            }
        }
    }
}
