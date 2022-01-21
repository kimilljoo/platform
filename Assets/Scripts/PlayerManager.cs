using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Action PlayerAction;

    [SerializeField]
    private GameObject shootPoint;

    [SerializeField]
    private GameObject bullet;

    private float moveSpeed = 3.0f;

    private float jumpPower = 5.5f;

    private Rigidbody2D rb;

    private Animator animator;

    SpriteRenderer spriteColor;

    private float curDamageDelayTime = 0.0f;
    private const float maxDamageDelayTime = 1.0f;

    private UiManager uiManager;

    public bool isStar;

    public bool isBall;
    public bool isJump;

    public float rayDist = 0.4f;

    public int dir = 1;

    
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
    }

    private void InitDeli()
    {
        PlayerAction += Jump;
        PlayerAction += Star;
        PlayerAction += Turn;
        PlayerAction += Death;
    }

    private void Update()
    {
        PlayerAction();
        AddDamageTime();
        ShootingBall();
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
    private void Jump()
    {
        if(Input.GetKey(KeyCode.Space) && isJump == false)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJump", true);
            isJump = true;
        }
    }
    private void CheckGround()
    {
        isJump = false;
        animator.SetBool("isJump", false);
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
            rayDist = 1.0f;
        }
        else
        {
            StopCoroutine("StarEffect");
            rayDist = 0.4f;
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
        curDamageDelayTime += Time.deltaTime;
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
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            CheckGround();
        }
        if ((collision.gameObject.CompareTag("Trap") && curDamageDelayTime >= maxDamageDelayTime ) && isStar == false)
        {
            giveDamage();
        }

    }
    public void giveDamage()
    {
        --GameManager.curHp;
        uiManager.SetHp();
        curDamageDelayTime = 0.0f;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isJump = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            Fall();
        }
        if (collision.gameObject.CompareTag("Monster") && CheckJump())
        {
            isJump = false;
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJump", true);
            isJump = true;
        }
        //else if(collision.gameObject.CompareTag("Monster") && isStar == false)
        //{
        //    giveDamage();
        //}
    }
    private void Fall()
    {
        giveDamage();
        transform.position = new Vector3(0, 0, -1);
    }


    










}
