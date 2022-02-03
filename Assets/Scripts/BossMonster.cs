using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public GameObject normalBullet;
    public GameObject guidedBullet;

    private void Start()
    {
        StartCoroutine("Pattern1");
    }

    private void Update()
    {
        
    }
    private IEnumerator Pattern1()
    {
        Instantiate(guidedBullet);
        yield return new WaitForSeconds(1f);
        StartCoroutine("Pattern1");
    }


}
