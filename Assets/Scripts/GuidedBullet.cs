using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedBullet : MonoBehaviour
{
    private float speed = 5;

    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);

        Vector3 dirVec = GameObject.FindWithTag("Player").transform.position - transform.position;
        rigid.AddForce(dirVec.normalized * speed, ForceMode2D.Impulse);
    }
}
