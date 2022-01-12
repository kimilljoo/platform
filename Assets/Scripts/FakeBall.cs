using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBall : MonoBehaviour
{
    [SerializeField]
    private GameObject Ball;
    private PlayerManager player;

    public bool isDestroy;

    private bool isStepable;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        StartCoroutine("OnStep");

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
        if((collision.gameObject.CompareTag("Player") && player.isJumping == true ) && isStepable == true)
        {
            Instantiate(Ball, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(isStepable == true)
        {
            StopCoroutine("OnStep");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            isDestroy = true;
        }
        else isDestroy = false;
    }
    private IEnumerator OnStep()
    {
        yield return new WaitForSeconds(0.5f);
        isStepable = true;
    }
}
