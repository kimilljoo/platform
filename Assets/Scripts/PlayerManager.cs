using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject shootPoint;

    private float moveSpeed = 3.0f;

    private float jumpPower = 5.5f;

    private Rigidbody2D rb;
    private BoxCollider2D bc;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private bool isJumping = false;

    public int maxHp = 3;
    public int curHp;

    private float curDamageDelayTime = 0.0f;
    private const float maxDamageDelayTime = 1.0f;

    private RaycastHit2D[] rayJumpHits;

    private UiManager uiManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
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
        if(collision.gameObject.CompareTag("DeathZone"))
        {
            --curHp;
            uiManager.SetHp(curHp, maxHp);
            transform.position = new Vector3(0, 0, -1);
        }
        Debug.Log(curHp);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (rayJumpHits[0].collider != null)
        {
            if (rayJumpHits[0].distance <= 0.5f)
            {
                Debug.Log("ray dis : " + rayJumpHits[0].distance);
                isJumping = false;
                animator.SetBool("isJump", false);
            }
        }
    }

    private void FixedUpdate()//�̵� 
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        Vector2 dir = new Vector2(moveHorizontal, 0);
        if ((dir.x == 1.0f || dir.x == -1.0f) && isJumping == false)
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
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                Debug.Log(isJumping);
            }
            else return;
        }
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Instantiate(bullet, shootPoint.transform.position, Quaternion.identity);
        }

        if(Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = moveHorizontal < 0;
        }
        
        curDamageDelayTime += Time.deltaTime;

        if(curHp == 0)
        {
            Destroy(gameObject);
        }
        
        float colliderX = bc.transform.position.x + bc.size.x;
        Vector2 rayPositionRight = new Vector2(colliderX, bc.transform.position.y);
        rayJumpHits[0] = Physics2D.Raycast(bc.transform.position, Vector2.down, 1, LayerMask.GetMask("Ground"));
        rayJumpHits[1] = Physics2D.Raycast(rayPositionRight, Vector2.down, 1, LayerMask.GetMask("Ground"));
        Debug.DrawRay(bc.transform.position, Vector2.down, Color.blue);
        Debug.DrawRay(rayPositionRight, Vector2.down, Color.blue);
        

    }
}
