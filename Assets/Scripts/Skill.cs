using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    public GameObject skillShootingPoint;
    [SerializeField] public float fireBallSpeed = 5;

    [SerializeField] GameObject fireBallPrefab;
    [SerializeField] GameObject iceBallPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireBallSkill"))
        {
            StartCoroutine(SkillCoroutine(skillShootingPoint,fireBallPrefab));
            Destroy(collision.gameObject);
        }

        if(collision.CompareTag("IceBallSkill"))
        {
            StartCoroutine(SkillCoroutine(skillShootingPoint,iceBallPrefab));
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator SkillCoroutine(GameObject shootingPoint, GameObject prefabbb)
    {
        float skillDuration = 5f;
        float startTime = Time.time;
        // Initial delay of 3 seconds
        yield return new WaitForSeconds(1f);

        while (Time.time < startTime + skillDuration)
        {
            InstantiateSkillObject(shootingPoint, prefabbb);
            yield return new WaitForSeconds(0.5f); // Interval between shots
        }
    }

    private void InstantiateSkillObject(GameObject shootingPoint, GameObject prefabb)
    {
        var skillObject = Instantiate(prefabb, shootingPoint.transform.position, shootingPoint.transform.rotation);
        skillObject.GetComponent<Rigidbody2D>().velocity = shootingPoint.transform.up * fireBallSpeed;
    }

}
