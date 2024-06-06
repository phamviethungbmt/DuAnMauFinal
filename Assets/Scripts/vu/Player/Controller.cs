using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
public class Controller : MonoBehaviour
{
   
    [SerializeField] private float walkSpeed = 0;
   [SerializeField] private float climbSpeed = 0;
    [SerializeField] private float runSpeed = 0;
    [SerializeField] private float airWalkSpeed = 0;
    [SerializeField] private float jumpImpulse = 0;
    [SerializeField] private int damage = 0;
  //  [SerializeField] private float distanceRaycast;
  //  [SerializeField] LayerMask whatIsLadder;

    TouchingDirection touchingDirection;

    Vector2 moveInput;
   
    private float currentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirection.IsOnWall)
                {
                    if (touchingDirection.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        // di chuyển khi ở trên kh
                        return airWalkSpeed;

                    }

                }
                else
                {
                    // Đứng im
                    return 0;
                }
            }

            else
            {
                //kh di chuyển đc
                return 0;
            }


        }
    }

    [SerializeField] private bool _isMoving = false;
    public bool IsMoving { get { return _isMoving; } private set { _isMoving = value; myAnimator.SetBool(AnimationString.isMoving, value); } }

    [SerializeField] private bool _isRunning = false;
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            myAnimator.SetBool(AnimationString.isRunning, value);
        }
    }
    [SerializeField] private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
        //        weapon.bulletPos.localScale *= new Vector2(-1, 1);
            }
           
            _isFacingRight = value;

        }
    }
    public bool CanMove { get { return myAnimator.GetBool(AnimationString.canMove); } }
    public bool IsAlive { get { return myAnimator.GetBool(AnimationString.isAlive); } }

 

    Rigidbody2D rb;
    Animator myAnimator;

    // climb
    [SerializeField] private LadderHandlers playerladder;
    [SerializeField] private LayerMaskCircle objectLadderLayerTop;
    [SerializeField] private LayerMaskCircle objectladderLayerBottom;
    PlayerHealth playerHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
      
    }
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        objectLadderLayerTop.HandleCollider2D();
        objectladderLayerBottom.HandleCollider2D();
    }
    private void FixedUpdate()
    { 
        rb.velocity = new Vector2(moveInput.x * currentMoveSpeed, rb.velocity.y);
        myAnimator.SetFloat(AnimationString.yVelocity, rb.velocity.y);

        if (IsMoving)
        {
            // rb.MovePosition(rb.position + ((new Vector2(moveInput.x * currentMoveSpeed * Time.fixedDeltaTime, rb.velocity.y))));
        }
        playerladder.onClimb(objectLadderLayerTop.IsCollider2D(),objectladderLayerBottom.IsCollider2D(),moveInput.y,moveInput.x,climbSpeed);
        

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }


    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirection.IsGrounded && CanMove)
        {
            myAnimator.SetTrigger(AnimationString.jumpTrigger);
            rb.AddForce(new Vector2(0, jumpImpulse), ForceMode2D.Impulse);
            //rb.velocity=new Vector2(rb.velocity.x,jumpImpulse)*Time.deltaTime;

        }
    }
    public void ỌnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            myAnimator.SetTrigger(AnimationString.attackTrigger);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemies=other.gameObject.GetComponent<Enemy>();

        if(enemies)
        {
            playerHealth.TakeDamage(damage);
        }    
    }
    //public Vector2 GetInput()
    //{
    //    return moveInput;
    //}
}
