using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{


    [SerializeField]
    private GameObject shootPoint;

    [SerializeField]
    private GameObject bullet;

    private float moveSpeed = 3.0f;

    private float jumpPower = 5.5f;

    private Rigidbody2D rb;
    private BoxCollider2D bc;

    private Animator animator;

    public bool isJumping = false;

    SpriteRenderer spriteColor;

    private float curDamageDelayTime = 0.0f;
    private const float maxDamageDelayTime = 1.0f;

    private RaycastHit2D[] rayJumpHits = new RaycastHit2D[3];

    private UiManager uiManager;

    public bool isStar;

    public bool isBall;

    public int dir = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        uiManager = GameObject.Find("UIManager").GetComponent<UiManager>();
        spriteColor = GetComponent<SpriteRenderer>();
        shootPoint.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if((collision.gameObject.CompareTag("Trap") && curDamageDelayTime >= maxDamageDelayTime ) && isStar == false)
        {
            giveDamage();
        }
        if(collision.gameObject.CompareTag("DeathZone"))
        {
            giveDamage();
            transform.position = new Vector3(0, 0, -1);
        }
        Debug.Log(GameManager.curHp);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") && isJumping == true)
        {
            isJumping = false;
            Jump();
        }
        else if(collision.gameObject.CompareTag("Monster") && isStar == false)
        {
            giveDamage();
        }

        if ((rayJumpHits[0].collider != null || rayJumpHits[1].collider != null) || rayJumpHits[2].collider != null)
        {
            if ((rayJumpHits[0].distance <= 0.5f || rayJumpHits[1].distance <= 0.5f)|| rayJumpHits[2].distance <= 0.5f)
            { 
            
                Debug.Log("ray dis left : " + rayJumpHits[0].distance);
                Debug.Log("ray dis right : " + rayJumpHits[1].distance);
                Debug.Log("ray dis player : " + rayJumpHits[2].distance);
                isJumping = false;
                animator.SetBool("isJump", false);
            }
        }
    }

    private void FixedUpdate()//ÀÌµ¿ 
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
            Jump();
        }
        
    }

    private IEnumerator StarEffect()
    {
        spriteColor.color = new Color(Random.Range(0.1f, 1.1f), Random.Range(0.1f, 1.1f), Random.Range(0.1f, 1.1f), 1);
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

    private void Update()
    {
        if(isBall == true)
        {
            shootPoint.gameObject.SetActive(true);
        }
        Vector2 rayLeft = new Vector2(bc.transform.position.x - (bc.size.x / 2), bc.transform.position.y);
        Vector2 rayRight = new Vector2(bc.transform.position.x + (bc.size.x / 2), bc.transform.position.y);
        transform.localScale = new Vector3(dir, 1);
        if (isStar)
        {
            rayRight = new Vector2(bc.transform.position.x + (bc.size.x / 2), bc.transform.position.y-1.5f);
            rayLeft = new Vector2(bc.transform.position.x - (bc.size.x / 2), bc.transform.position.y-1.5f);
            StartCoroutine("StarEffect");
        }
        else
        {
            
            rayRight = new Vector2(bc.transform.position.x + (bc.size.x / 2), bc.transform.position.y);
            rayLeft = new Vector2(bc.transform.position.x - (bc.size.x / 2) , bc.transform.position.y);
            StopCoroutine("StarEffect");
        }
        if(Input.GetKey(KeyCode.LeftControl) && isBall == true) 
        {
            Instantiate(bullet,shootPoint.transform.position,Quaternion.identity);
            isBall = false;
        }
        else if(isBall == false)
        {
            shootPoint.gameObject.SetActive(false);
        }

        Debug.Log(isJumping);
        


        if (Input.GetKey(KeyCode.RightArrow))
        {
            dir = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir = -1;
        }
        
        curDamageDelayTime += Time.deltaTime;

        if(GameManager.curHp == 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(4);
        }

        rayJumpHits[0] = Physics2D.Raycast(rayLeft, Vector2.down, 1, LayerMask.GetMask("Ground"));
        rayJumpHits[1] = Physics2D.Raycast(rayRight, Vector2.down, 1, LayerMask.GetMask("Ground"));
        rayJumpHits[2] = Physics2D.Raycast(bc.transform.position, Vector2.down, 1, LayerMask.GetMask("Ground"));

        Debug.DrawRay(rayLeft, Vector2.down, Color.blue);
        Debug.DrawRay(rayRight, Vector2.down, Color.blue);
        Debug.DrawRay(bc.transform.position, Vector2.down, Color.blue);

    }
    public void giveDamage()
    {
        --GameManager.curHp;
        uiManager.SetHp();
        curDamageDelayTime = 0.0f;
    }
    public void Jump()
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
