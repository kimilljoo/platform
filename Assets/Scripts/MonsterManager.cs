using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fakeBall;

    private int[] randDir = { -1, 1 };
    private int randNum = 0;
    private float moveSpeed = 2.0f;

    PlayerManager playerManager;

    private void Awake()
    {
        randNum = Random.Range(0, 2);
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.CompareTag("Player") && playerManager.CheckJump()== true) || (collision.gameObject.CompareTag("Player") && playerManager.isStar == true))
        {
            GameManager.score += 1000;
            if(gameObject.name == "Armadillo")
            {
                Instantiate(fakeBall, gameObject.transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
            
        }
        
        if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Monster"))
        {
            randDir[randNum] *= -1;
        }
        
    
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(randDir[randNum], 0)*Time.deltaTime*moveSpeed);
    }
    private void Update()
    {
        
        transform.localScale = new Vector3(randDir[randNum]*-1, 1, 1);
    }
}
