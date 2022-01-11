using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private float speed = 4.0f;
    private PlayerManager player;
    private int dir;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        dir = player.dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
        {
            dir *= -1;
        }
    }
    private void Update()
    {
        transform.Translate(new Vector2(dir,0) * Time.deltaTime * speed);

    }
}
