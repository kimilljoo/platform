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
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(new Vector2(player.transform.localScale.x,0) * Time.deltaTime * speed);

    }
}
