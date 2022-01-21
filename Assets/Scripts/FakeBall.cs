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
        Init();
    }
    private void Init()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        StartCoroutine("OnStep");

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")&& isDestroy == true)
        {
            SetBall();
        }
    }

    private  void SetBall()
    {
        player.isBall = true;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") && player.isJump) && isStepable == true)
        {
            ToStepOn();
        }
    }

    private void ToStepOn()
    {
        Instantiate(Ball, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Update()
    {
        DelayBall();
        GetBall();
        
    }
    private void DelayBall()
    {
        if (isStepable == true)
        {
            StopCoroutine("OnStep");
        }
    }

    private void GetBall()
    {
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
