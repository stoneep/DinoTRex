using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 12f;
    public float jumpForce;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public PlayerStateMachine StateMachine { get; private set; }
    
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    
  //  private float jumpHeight = 2f;
    
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, StateMachine, "Idle");
        moveState = new PlayerMoveState(this, StateMachine, "Move");
        jumpState = new PlayerJumpState(this, StateMachine, "Jump");
        airState = new PlayerAirState(this, StateMachine, "Jump");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        StateMachine.Initialize(idleState);
    }

    private void Update()
    {
        StateMachine.currentState.Update();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}






// protected override void Update()
// {
//     anim.SetBool("isRun", true);
//         
//     if (Input.GetKeyDown(KeyCode.Space))
//     {
//         isJump = true;
//     }
//     else if (rb.position.y < startPosition.y)
//     {
//         isJump = false;
//         isTop = false;
//         transform.position = startPosition;
//     }
//
//     if (isJump)
//     {
//         if (rb.position.y <= jumpHeight - 0.1f && !isTop)
//             transform.position = Vector2.Lerp(transform.position,new Vector2(transform.position.x, jumpHeight), jumpSpeed * Time.deltaTime);
//     }
//     else
//     {
//         isTop = true;
//     }
//
//     if (transform.position.y > startPosition.y && isTop)
//     {
//         transform.position = Vector2.MoveTowards(transform.position, startPosition, jumpSpeed * Time.deltaTime);
//     }
// }