using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform toFlip;
    private Vector2 toFlipPos;



    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float JumpPower;

    [SerializeField]
    private int totalJump;

    Rigidbody2D rb;

    public bool isGrounded;

    public int airCount;

    [SerializeField]
    private Animator animator;

    public bool isJump;

    public static PlayerMovement instance;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private string axisName;

    [SerializeField]
    private KeyCode jumpKey;
    [SerializeField]
    private KeyCode dashKey;

    [Header("Knockback")]
    [SerializeField] private Transform _center;
    [SerializeField] private float _knockbackVel = 8f;
    [SerializeField] private float _knockbackTime = 1f;
    [SerializeField] private bool _knockbacked;


    [SerializeField] private TrailRenderer tr;


    private bool canDash = true;
    [SerializeField]
    private float dashingPowerMultiply = 0f;
    [SerializeField]
    private float dashingTime = 0.5f;
    [SerializeField]
    private float dashingCooldown = 1f;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        toFlipPos = toFlip.localScale;
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Move();

        Jump();

        Dash();
    }


    private void Move()
    {
        float x = Input.GetAxis(axisName);
        Vector2 direction = new Vector2(x, 0);

        if (x != 0 && isGrounded)
        {
            SetAnimParam(true, false);
        }

        if (x == 0 && isGrounded)
        {
            SetAnimParam(false, false);
            //  MoveBackground.instance.Move(0, false);
        }
        transform.Translate(direction * Time.deltaTime * speed);



        if (x < 0)
        {
            Facing(false);
        }

        if (x > 0)
        {
            Facing(true);
        }
    }

    private void Jump()
    {
        //single jump
        /*
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector2 direction = new Vector2(0, 1);
            rb.velocity = direction * JumpPower;
        }*/


        //double jump
        if (Input.GetKeyDown(jumpKey) && airCount < totalJump)
        {
            Jumping();
            airCount += 1;
        }
    }

    private void Jumping()
    {
        //AudioPlayer.instance.PlaySFX(0);
        SetAnimParam(false, true);

        Vector2 direction = new Vector2(0, 1);
        rb.velocity = direction * JumpPower;
    }

    private void Dash()
    {

        if (canDash)
        {
            if (Input.GetKey(dashKey))
            {
                if (dashingPowerMultiply < 15f)
                {
                    dashingPowerMultiply += Time.deltaTime * 15;
                }

            }

            if (Input.GetKeyUp(dashKey))
            {
                StartCoroutine(Dashing());
            }
        }

        
    }

    private void Facing(bool isFacingRight)
    {
        if (isFacingRight)
        {
            // transform.localScale = new Vector3(1, 1, 1);

            toFlip.localScale = new Vector2(toFlipPos.x, toFlipPos.y);
            //flipX sprite
            spriteRenderer.flipX = false;
            return;
        }
        if (!isFacingRight)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            toFlip.localScale = new Vector2(toFlipPos.x * -1, toFlipPos.y);
            //flipX sprite
            spriteRenderer.flipX = true;
            return;
        }
    }


    private void SetAnimParam(bool isRunning, bool isJumping)
    {
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Knockback(Transform t, float knockbackVal)
    {
        _knockbackVel = knockbackVal;
        var dir = _center.position - t.position;
        _knockbacked = true;
        rb.velocity = dir.normalized * _knockbackVel;
    }

    private IEnumerator Dashing()
    {
        canDash = false;
        //isDashing = true;
        float orSpeed = speed;
        speed = speed * dashingPowerMultiply;

        //float orJumpower = JumpPower;
        //JumpPower = JumpPower * 3;

        //StartCoroutine(JumpDash());
        tr.emitting = true;



        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        //isDashing = false;
        speed = orSpeed;
        dashingPowerMultiply = 2f;
        //JumpPower = orJumpower;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private IEnumerator JumpDash()
    {
       

        float posY1 = transform.position.y;
        yield return new WaitForSeconds(0.05f);
        float posY2 = transform.position.y;

        if (posY1 < posY2)
        {
            Debug.Log("jumping");
            Jumping();
        }
        if (posY1 > posY2)
        {
            Debug.Log("falling");
        }

    }
}
