using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] GameObject fireBallPrefab;
    //[SerializeField] GameObject shieldPrefab;
    //[SerializeField] GameObject iceBlastPrefab;

    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    public GameObject shootingPoint1;
    public GameObject shootingPoint2;
    [SerializeField] public float fireBallSpeed = 5;

    public static List<string> player1Skills = new List<string>(); // Store multiple skills
    public static List<string> player2Skills = new List<string>();

    public static string player1SkillsString = "";
    public static string player2SkillsString = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player1Skills.Contains("Fireball"))
        {
            print("Player1 has fire ball");
            StartCoroutine(FireballCoroutine(shootingPoint1));
        }

        if (player2Skills.Contains("Fireball"))
        {
            print("Player2 has fire ball");
            StartCoroutine(FireballCoroutine(shootingPoint2));
        }
    }

    private IEnumerator FireballCoroutine(GameObject shootingPoint)
    {
        // Initial delay of 3 seconds
        yield return new WaitForSeconds(3f);

        while (true) // Keep firing fireballs every 5 seconds
        {
            Fireball(shootingPoint);
            yield return new WaitForSeconds(5f);
        }
    }

    private void Fireball(GameObject shootingPoint)
    {
        var fireBall = Instantiate(fireBallPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
        fireBall.GetComponent<Rigidbody2D>().velocity = shootingPoint.transform.up * fireBallSpeed;
    }

    private void Shield()
    {

    }

    private void Freeze()
    {

    }

    private void IceBlast()
    {

    }

    private void Posion()
    {

    }

    public string printPlayer1Skills()
    {
        int player1SkillNumber = player1Skills.Count;



        for (int i = 0; i < player1SkillNumber; i++)
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