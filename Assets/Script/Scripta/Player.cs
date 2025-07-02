using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 4f;
    private float moveDirection;          // -1, 0, +1
    private bool isDashing;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashTime = 0.25f;

    [Header("Attack")]
    public Transform attackPt;            // empty child in front of sprite

    [Header("Buttons")]
    public Button slashButton;
    public Button dashButton;
    
     Rigidbody2D body;
    Animator anim;
    float idleTimer;
    const float idleThreshold = 2f;
    bool isDead;

    void Awake()
    {
        if (instance == null) instance = this; else { Destroy(gameObject); return; }
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
       // slashButton?.onClick.AddListener(HandleSlashInput);
     //   dashButton?.onClick.AddListener(HandleDashInput);
    }
    void Update()
    {
       
        if (isDead) return;
        moveDirection = 0;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            moveDirection = -1;
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            moveDirection = 1;

        // --- Slash and Dash ---
        if (Input.GetKeyDown(KeyCode.Z))
            HandleSlashInput();
        if (Input.GetKeyDown(KeyCode.X))
            HandleDashInput();
        HandleFlip();
        HandleIdleLogic();
    }

    void HandleIdleLogic()
    {
        if (Mathf.Abs(moveDirection) < 0.01f && !isDashing)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleThreshold)
            {
               SliderColor.instance?.OnIdle();
                idleTimer = 0;
            }
        }
        else idleTimer = 0;
    }
    void FixedUpdate()
    {
        if (isDead) return;

        if (!isDashing)
            body.velocity = new Vector2(moveDirection * moveSpeed, body.velocity.y);
        anim.SetBool("isRunning", moveDirection != 0);
    }
     public void HandleDashInput()
    {
        if (isDead || isDashing) return;
        moveDirection = 0;
        isDashing = true;
        anim.SetTrigger("Dash");
        Vector2 dashVec = new Vector2(transform.localScale.x, 0).normalized;
        body.velocity = dashVec * dashForce;
        SliderColor.instance?.OnDash();
        Invoke(nameof(EndDash), dashTime);
    }

    void EndDash()
    {
        isDashing = false;

        moveDirection = 0;             
        body.velocity = Vector2.zero;
    }
    public void HandleSlashInput()
    {
        if (isDead) return;
        anim.SetTrigger("Slash");
        SliderColor.instance?.OnSlash();
        
    }

    public void MovingLeft()
    {
        moveDirection = -1;
    }
    public void MovingRight()
    {
        moveDirection = 1;
    }
    public void MovingStop()
    {
        moveDirection = 0;
    }
    void HandleFlip()
    {
        if (moveDirection > 0.1f)
        {
            transform.localScale = Vector3.one;
            attackPt.localPosition = new Vector3(Mathf.Abs(attackPt.localPosition.x),
                                                 attackPt.localPosition.y);
        }
        else if (moveDirection < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            attackPt.localPosition = new Vector3(-Mathf.Abs(attackPt.localPosition.x),
                                                 attackPt.localPosition.y);
        }
    }

    public void PlayerHitAnimation() => anim.SetTrigger("Hit");
    public void PlayerDeathAnimation() { isDead = true; anim.SetTrigger("Death"); enabled = false; }
   
}
