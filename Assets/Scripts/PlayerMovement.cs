using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int playerIndex;

    [SerializeField]
    private Transform toFlip;
    private Vector2 toFlipPos;

    [SerializeField]
    private Image cooldown;
    [SerializeField]
    private Image dashImage;


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
    [SerializeField]
    private Animator camAnimator;

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

    [SerializeField]
    KnockbackTrigger knockbackTrigger;
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
        if(playerIndex == 1)
        {
            AudioPlayer.instance.PlaySFX(0);
        }
        if(playerIndex == 2)
        {
            AudioPlayer.instance.PlaySFX2(0);
        }
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
                if (dashingPowerMultiply < 5f)
                {
                    cooldown.transform.gameObject.SetActive(false);
                    dashingPowerMultiply += Time.deltaTime * 5;
                    dashImage.fillAmount = dashingPowerMultiply / 5f;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Batas")
        {
            camAnimator.SetTrigger("Knock");
            if (playerIndex == 1)
            {
                AudioPlayer.instance.PlaySFX(3);
            }
            if (playerIndex == 2)
            {
                AudioPlayer.instance.PlaySFX2(3);
            }
        }
    }

    public void Knockback(Transform t, float knockbackVal)
    {
        camAnimator.SetTrigger("Knock");
        if (playerIndex == 1)
        {
            AudioPlayer.instance.PlaySFX(3);
        }
        if (playerIndex == 2)
        {
            AudioPlayer.instance.PlaySFX2(3);
        }
        _knockbackVel = knockbackVal;
        var dir = _center.position - t.position;
        _knockbacked = true;
        rb.velocity = dir.normalized * _knockbackVel;
    }

    public void LandSound()
    {
        if (playerIndex == 1)
        {
            AudioPlayer.instance.PlaySFX(4);
        }
        if (playerIndex == 2)
        {
            AudioPlayer.instance.PlaySFX2(4);
        }
    }

    private IEnumerator Dashing()
    {
        if (playerIndex == 1)
        {
            AudioPlayer.instance.PlaySFX(2);
        }
        if (playerIndex == 2)
        {
            AudioPlayer.instance.PlaySFX2(2);
        }
        knockbackTrigger.gameObject.SetActive(true);
        canDash = false;
        //isDashing = true;
        float orSpeed = speed;
        speed = speed * dashingPowerMultiply;

        //float orJumpower = JumpPower;
        //JumpPower = JumpPower * 3;

        //StartCoroutine(JumpDash());
        tr.emitting = true;



        yield return new WaitForSeconds(dashingTime);
        
        //isDashing = false;
        speed = orSpeed;
        dashingPowerMultiply = 2f;
        dashImage.fillAmount = 0f;
        //JumpPower = orJumpower;
        knockbackTrigger.gameObject.SetActive(false);
        tr.emitting = false;
        yield return new WaitForSeconds(dashingCooldown);
        cooldown.transform.gameObject.SetActive(true);
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
