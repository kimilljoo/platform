using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBall : MonoBehaviour
{
    [SerializeField]
    private GameObject Ball;
    private PlayerManager player;

    public bool isDestroy;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")&& isDestroy == true)
        {
            player.isBall = true;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.CompareTag("Player") && player.isJumping == true ) && player.isStepable == true)
        {
            player.isStepable = false;
            player.countJump = 0;
            Instantiate(Ball, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            isDestroy = true;
        }
        else isDestroy = false;
    }

}
