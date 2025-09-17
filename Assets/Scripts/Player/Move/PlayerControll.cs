using System;
using System.Runtime.CompilerServices;
using Components;
using Components.Container;
using Systems;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerControll : BasePlayerInputSystem, IMovable
{
    [SerializeField]
    Rigidbody2D _body;
    [SerializeField]
    Transform _groundCheckPosition;
    [Range(0, 20)]
    [SerializeField]
    private float _moveSpeed;
    [Range(0, 20)]
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private Animator animator;
    private bool _isGrounded => Physics2D.Raycast(_groundCheckPosition.position, Vector2.down, 0.2f);
    private Vector2 _moveDirection = Vector2.zero;

    public float MoveSpeed => _moveSpeed;
    public float JumpForce => _jumpForce;
    public bool IsGrounded => _isGrounded;
    public bool IsMoving => _moveDirection != Vector2.zero ? true : false;
    public Vector2 MoveDirection => _moveDirection;
    public static Action InteractAction;

    public override void Initialize(IObjectsContainer container)
    {
        base.Initialize(container);
        onInitializeCompelted();
    }

    private void onInitializeCompelted()
    {
        Move.performed += OnMovePerformed;
        Move.canceled += OnMoveCanceled;
        Jump.performed += OnJumpPerformed;

        Interact.performed += OnInteractPerforemed;
        Attack.performed += OnAttackPerforemed;

    }

    private void OnMovePerformed(CallbackContext cbc)
    {
        animator.SetTrigger("IsWalk");
        _moveDirection = new Vector2(cbc.ReadValue<Vector2>().x, _body.linearVelocityY);
    }

    private void OnMoveCanceled(CallbackContext cbc)
    {
        animator.ResetTrigger("IsWalk");
        _moveDirection = Vector2.zero;
    }

    private void OnJumpPerformed(CallbackContext cbc)
    {
        if (!_isGrounded) return;

        _body.linearVelocity = new Vector2(_body.linearVelocity.x, 0);
        _body.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        RaycastHit2D d2 = Physics2D.Raycast(_groundCheckPosition.position, Vector2.down, 0.2f);
        Debug.Log(d2.transform.name);
    }

    private void OnInteractPerforemed(CallbackContext cbc)
    {
        Debug.Log("interact");
    }

    private void OnAttackPerforemed(CallbackContext cbc)
    {
        Debug.Log("Attack");
        Collider2D[] colliders = Physics2D.OverlapAreaAll(Vector2.zero, new Vector2(2, 2));

        Debug.Log("local" + transform.localPosition);
    }

    private void FixedUpdate()
    {
        Vector2 move = new Vector2(_moveDirection.x * _moveSpeed, _body.linearVelocity.y);
        _body.linearVelocityX = move.x;
    }

    private void OnDrawGizmos()
    {
        Vector2 pointA = Vector2.zero; // (0, -1)
        Vector2 pointB = new Vector2(2, 2);   // (0, 1)

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((pointA + pointB) / 2f, pointB - pointA);
        Gizmos.DrawRay(_groundCheckPosition.position, Vector2.down * 0.2f);
        // Physics2D.Raycast(_groundCheckPosition.position, Vector2.down, 0.2f)
    }
}
