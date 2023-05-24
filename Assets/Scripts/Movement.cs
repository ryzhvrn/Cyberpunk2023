using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private const string Speed = "Speed";
    private const string IsGrounded = "IsGrounded";
    private const string YVelocity = "yVelocity";
    private const string IsWallSliding = "IsWallSliding";

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 800f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsWall;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private float _wallCheckDistance = 0.2f;
    [SerializeField] private float _xWallJumpForce = 0;
    [SerializeField] private float _yWallJumpForce = 0;

    private Rigidbody2D _rigidbody;
    private float _movingInput;
    private Animator _animator;
    private bool _isFacingRight = true;
    private float _groundCheckRadius = 0.2f;
    private bool _isGrounded = false;
    private bool _canDoubleJump;
    private bool _isWallDetected;
    private bool _canWallSlide;
    private bool _isWallSliding;
    private int _facingDirection = 1;
    private float _slidingSpeed = 0.1f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        WallSlide();
        WallJump();
    }

    private void Move()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);
        _movingInput = Input.GetAxisRaw("Horizontal");
        _animator.SetFloat(Speed, Mathf.Abs(_movingInput));
        _animator.SetFloat(YVelocity, _rigidbody.velocity.y);
        _rigidbody.velocity = new Vector2(_movingInput * _speed, _rigidbody.velocity.y);

        if (_isGrounded)
        {
            _canDoubleJump = false;
        }

        Jump();

        if (_movingInput > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (_movingInput < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    private void Jump()
    {
        _animator.SetBool(IsGrounded, _isGrounded);

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_isGrounded)
            {
                ApplyJumpForce();
            }
            else if (!_canDoubleJump)
            {
                _canDoubleJump = true;
                ApplyJumpForce();
            }
        }
    }

    private void ApplyJumpForce()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }


    private void WallSlide()
    {
        _isWallDetected = Physics2D.Raycast(_wallCheck.position, Vector2.right * transform.localScale.x,
            _wallCheckDistance, _whatIsGround);

        if (_isWallDetected)
        {
            _isWallSliding = true;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * _slidingSpeed);
        }
        else
        {
            _isWallSliding = false;
        }

        _animator.SetBool(IsWallSliding, _isWallSliding);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void WallJump()
    {
        if (_isWallSliding && Input.GetKeyDown(KeyCode.W))
        {
            _rigidbody.velocity = new Vector2(_xWallJumpForce * -_facingDirection, _yWallJumpForce);
        }
    }
}
