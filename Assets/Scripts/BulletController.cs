using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 10.0f;
    private PlayerManager player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);

    }
}
