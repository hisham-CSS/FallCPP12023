using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //Component References
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    Coroutine jumpForceChange;


    private int _score = 0;
    public int score
    {
        get => _score;
        set
        {
            _score = value;
            Debug.Log("Score has ben set to: " + _score.ToString());
        }
    }

    private int _lives = 3;

    public int lives
    {
        get => _lives;
        set
        {
            //if (_lives > value)
            //Respawn = lost a life

            _lives = value;

            if (_lives > maxLives)
                _lives = maxLives;

            //if (_lives < 0)
            //gameover

            Debug.Log("Lives has ben set to: " + _lives.ToString());
        }
    }
    public int maxLives = 5;
    
    //Movement variables
    public float speed = 5.0f;
    public float jumpForce = 300.0f;

    //GroundCheck stuff
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        //Getting our component references
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //checking variables for dirty data
        if (rb == null) Debug.Log("No RigidBody Reference");
        if (sr == null) Debug.Log("No Sprite Renderer Reference");
        if (anim == null) Debug.Log("No animator reference");
        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
            Debug.Log("groundcheck set to default value");
        }
        if (speed <= 0)
        {
            speed = 5.0f;
            Debug.Log("speed set to default value");
        }
        if (jumpForce <= 0)
        {
            jumpForce = 300.0f;
            Debug.Log("jumpforce set to default value");
        }
        if (groundCheck == null)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            groundCheck = obj.transform;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxisRaw("Horizontal");
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        if (isGrounded) rb.gravityScale = 1;
        

        if (curPlayingClips.Length > 0)
        {
            if (curPlayingClips[0].clip.name == "Fire")
                rb.velocity = Vector2.zero;
            else
            {
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
        }


        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (!isGrounded && Input.GetButtonDown("Jump"))
            anim.SetTrigger("JumpAttack");



        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Fire");
        }

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);

        //flip functionality
        if (hInput != 0) sr.flipX = (hInput < 0);
    }

    public void IncreaseGravity()
    {
        rb.gravityScale = 10;
    }

    public void StartJumpForceChange()
    {
        if (jumpForceChange == null) jumpForceChange = StartCoroutine(JumpForceChange());
        else
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce /= 2;
            jumpForceChange = StartCoroutine(JumpForceChange());
        }

    }
    
    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;
        yield return new WaitForSeconds(5.0f);
        jumpForce /= 2;
        jumpForceChange = null;
    }

    //called the frame that the collision happens
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    //called the frame that the collision exits
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    
    //called on the second frame while in the collision, and continously called while you remain in the collider.
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
