using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private float moveSpeed = 3.0f;

    private float jumpPower = 5.0f;

    private Rigidbody2D rb;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private bool isJumping = false;

    private const int maxHp = 3;
    private int playerHp;

    private bool isMove = true;

    private bool isDamage;
    private bool isDamageAble;
    private const float maxDamageDelayTime = 1.0f;
    private float curDamageDelayTime = 0.0f;

    private RaycastHit2D raycastJumpHit;
    private RaycastHit2D raycastLeftHit;
    private RaycastHit2D raycastRightHit;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        playerHp = maxHp;


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isDamage = true;
        if(collision.gameObject.CompareTag("Trap") && isDamageAble == true)
        {
            --playerHp;
        }
        isDamageAble = false;
        Debug.Log(playerHp);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (raycastJumpHit.collider != null)
        {
            if (raycastJumpHit.distance <= 0.5)
            {
                Debug.Log(isJumping);
                isJumping = false;
                animator.SetBool("isJump", false);
            }
        }
    }

    private void Update()
    {

        if(isDamage == true)
        {
            curDamageDelayTime += Time.deltaTime;
            if(curDamageDelayTime >= maxDamageDelayTime)
            {
                curDamageDelayTime = 0.0f;
                isDamageAble = true;
                isDamage = false;
            }
        }

        if(playerHp == 0)
        {
            Destroy(gameObject);
        }

        raycastJumpHit = Physics2D.Raycast(rb.position, Vector2.down, 1, LayerMask.GetMask("Ground"));
        raycastLeftHit = Physics2D.Raycast(rb.position, Vector2.left, 1, LayerMask.GetMask("Ground"));
        raycastRightHit = Physics2D.Raycast(rb.position, Vector2.right, 1, LayerMask.GetMask("Ground"));

        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = moveHorizontal == -1;
        }

        Vector2 dir = new Vector2(moveHorizontal, 0);

        if(dir.x == 1.0f || dir.x == -1.0f)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        if(raycastLeftHit.collider != null || raycastRightHit.collider != null)
        {
            if(raycastLeftHit.distance ==0.0f || raycastRightHit.distance == 0.0f)
            {
                isMove = false;
            }
        }
        else
        {
            isMove = true;
        }
        if(isMove == true)
        {
            transform.Translate(dir * Time.deltaTime * moveSpeed);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJumping == false)
            {
                animator.SetBool("isJump", true);
                isJumping = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpPower);
                Debug.Log(isJumping);
            }
            else return;
        }

    }
}
