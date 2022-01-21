using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private float speed = 4.0f;
    private PlayerManager player;
    private int dir;
    private Rigidbody2D rb;

    private float jumpPower = 3.0f;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        dir = player.dir;
        dir = player.dir * -1;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player")))
        {
            dir *= -1;
        }
    }

    private void Update()
    {
        transform.Translate(new Vector2(dir,0) * Time.deltaTime * speed);

    }

    
}
