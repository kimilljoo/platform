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

    public int maxHp = 3;
    public int curHp;

    private float curDamageDelayTime = 0.0f;
    private const float maxDamageDelayTime = 1.0f;

    private RaycastHit2D raycastJumpHit;

    private UiManager uiManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        uiManager = GameObject.Find("UIManager").GetComponent<UiManager>();
        curHp = maxHp;


    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Trap") && curDamageDelayTime >= maxDamageDelayTime)
        {
            --curHp;
            uiManager.SetHp(curHp, maxHp);
            curDamageDelayTime = 0.0f;
        }
        Debug.Log(curHp);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (raycastJumpHit.collider != null)
        {
            if (raycastJumpHit.distance <= 10.0f)
            {
                isJumping = false;
                animator.SetBool("isJump", false);
            }
        }
    }

    private void Update()
    {
        Debug.Log(isJumping);
        
        curDamageDelayTime += Time.deltaTime;

        if(curHp == 0)
        {
            Destroy(gameObject);
        }

        raycastJumpHit = Physics2D.Raycast(rb.position, Vector2.down, 1, LayerMask.GetMask("Ground"));

        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = moveHorizontal < 0;
        }
        Vector2 dir = new Vector2(moveHorizontal, 0);

        if((dir.x == 1.0f || dir.x == -1.0f) && isJumping == false)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        transform.Translate(dir * Time.deltaTime * moveSpeed);


        if (Input.GetKey(KeyCode.Space))
        {
            if (isJumping == false)
            {
              animator.SetBool("isJump", true);
                animator.SetBool("isMove", false);
                isJumping = true;
              GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpPower);
              Debug.Log(isJumping);
            }
            else return;
        }

    }
}
