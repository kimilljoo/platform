using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;
    private BoxCollider2D boxCollider2D;
    public bool isGrounded;
    private Vector3 footPosition;

    private Action PlayerAction;

    [SerializeField]
    private GameObject shootPoint;

    [SerializeField]
    private GameObject bullet;

    private float moveSpeed = 3.0f;

    private float jumpPower = 5.3f;

    private Rigidbody2D rb;

    private Animator animator;

    SpriteRenderer spriteColor;

    private UiManager uiManager;

    public bool isStar;
    public bool isInvincibility;

    public bool isBall;
    public bool isJump;


    public int dir = 1;

    public bool isTrap = false;
    private void Start()
    {
        Init();
        InitDeli();

    }
    private void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        uiManager = GameObject.Find("UIManager").GetComponent<UiManager>();
        spriteColor = GetComponent<SpriteRenderer>();
        shootPoint.gameObject.SetActive(false);
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    private void InitDeli()
    {
        PlayerAction += Star;
        PlayerAction += Turn;
        PlayerAction += Death;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        PlayerAction();
        AddDamageTime();
        ShootingBall();

        if (isTrap == true && isInvincibility == false)
        {
            giveDamage();
        }
        if (isInvincibility == false)
        {
            StopCoroutine(Invincibility());
        }

    }
    public void Turn()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dir = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir = -1;
        }
        transform.localScale = new Vector3(dir, 1);
    }
    public void Jump()
    {
       rb.velocity = Vector2.up * jumpPower;
       animator.SetBool("isJump", true);
       isJump = true;
    
    }
    private void CheckGround()
    {
        Bounds bounds = boxCollider2D.bounds;

        footPosition = new Vector2(bounds.center.x, bounds.min.y);

        isGrounded = Physics2D.OverlapCircle(footPosition, 0.12f, groundLayer);

        if(isGrounded)
        {
            animator.SetBool("isJump", false);
        }

    }

    public bool CheckJump()
    {
        bool isJump;
        if(animator.GetBool("isJump"))
        {
            isJump = true;
        }
        else
        {

            isJump = false;
        }
        return isJump;

    }
    private void Star()
    {
        if (isStar)
        {
            StartCoroutine("StarEffect");
            
        }
        else
        {
            StopCoroutine("StarEffect");
            
        }
    }
    private IEnumerator StarEffect()
    {
        spriteColor.color = new Color(UnityEngine.Random.Range(0.1f, 1.1f), UnityEngine.Random.Range(0.1f, 1.1f), UnityEngine.Random.Range(0.1f, 1.1f), 1);
        for (float i = 1.0f; i <= 1.5f; i+=0.005f)
        {
            transform.localScale = new Vector3(dir * i, i);
            yield return null;
        }
        
        yield return new WaitForSeconds(5f);
        spriteColor.color = new Color(1.0f, 1.0f, 1.0f);
        transform.localScale = new Vector3(dir, 1);
        isStar = false;
    }
    private void Death()
    {
        if (GameManager.curHp == 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(4);
        }
    }
    private void AddDamageTime()
    {
    }
    private void ShootingBall()
    {
        if (isBall == true)
        {
            shootPoint.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.LeftControl) && isBall == true)
        {
            Instantiate(bullet, shootPoint.transform.position, Quaternion.identity);
            isBall = false;
        }
        else if (isBall == false)
        {
            shootPoint.gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        Move();

        CheckGround();

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(footPosition, 0.12f);
    }
    private void Move()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        Vector2 dir = new Vector2(moveHorizontal, 0);
        if ((dir.x == 1.0f || dir.x == -1.0f) && !CheckJump())
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        transform.Translate(dir * Time.deltaTime * moveSpeed);
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Trap")&& isStar == false) && isInvincibility == false)
        {
            giveDamage();
        }

    }
    public void giveDamage()
    {
        --GameManager.curHp;
        uiManager.SetHp();
        StartCoroutine(Invincibility());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            Fall();
        }

        if(collision.gameObject.CompareTag("Trap") && isStar == false)
        {
            isTrap = true;
        }

        if(collision.gameObject.CompareTag("Bullet") && isInvincibility == false)
        {
            giveDamage();
            Destroy(collision.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Trap") && isStar == false)
        {
            isTrap = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Monster") && isStar == false) && isInvincibility == false)
        {
            giveDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") && isGrounded==false)
        {
            Jump();
        }
        
    }
    private void Fall()
    {
        giveDamage();
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            transform.position = new Vector3(0, -3, -1);
        }
        else
        {
            transform.position = new Vector3(0, 0, -1);
        }
    }


    private IEnumerator Invincibility()
    {
        isInvincibility = true;
        spriteColor.color = new Color(1.0f, 1.0f, 1.0f,0.6f);
        yield return new WaitForSeconds(0.6f);
        spriteColor.color = new Color(1.0f, 1.0f, 1.0f,1f);
        isInvincibility = false;
    }


}
