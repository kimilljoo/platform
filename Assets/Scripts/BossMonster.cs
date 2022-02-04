using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public GameObject normalBullet;
    public GameObject guidedBullet;
    private int patternNum;

    public GameObject shootingPoint;

    private float rotateSpeed = 4f;

    private void Start()
    {
        StartCoroutine("Pattern1");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.score += 10000;
        }
    }

    private void Update()
    {
    }

    private IEnumerator Pattern1()
    {
        yield return new WaitForSeconds(2f);
        circle();
        patternNum = Random.Range(1, 3);
        StartCoroutine("Pattern" + patternNum.ToString());
    }

    private IEnumerator Pattern2()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(guidedBullet);
        patternNum = Random.Range(1, 3);
        StartCoroutine("Pattern" + patternNum.ToString());
    }

    public void circle()
    {
        for(int i = 0; i<360; i+= 26)
        {
            GameObject bullet = Instantiate(normalBullet);
            bullet.transform.position = shootingPoint.transform.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }

    public void spin()
    {
        shootingPoint.transform.Rotate(Vector3.forward * rotateSpeed * 100 * Time.deltaTime);

        GameObject bullet = Instantiate(normalBullet);

        bullet.transform.position = shootingPoint.transform.position;

        bullet.transform.rotation = shootingPoint.transform.rotation;
    }
}
