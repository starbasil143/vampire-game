using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _dashSpeed = 90f;
    [SerializeField] private float _dashCooldown = 1f;
    [SerializeField] private float _dashLength = .2f;
    private float _currentDashCooldown = 0f;
    private bool _isDashing;
    private Vector2 _lastDirection;

    private Vector2 _movement;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Walking
        if(!_isDashing)
        {
            _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
            _rigidbody.velocity = _movement * _moveSpeed;
        }

        // Dashing
        if (InputManager.Dash && _currentDashCooldown <= 0)
        {
            _isDashing = true;
            _currentDashCooldown = _dashCooldown;
            _movement = _lastDirection;
        }
        if (_isDashing)
        {
            _rigidbody.velocity = _movement * Mathf.Lerp(_dashSpeed, _moveSpeed, (_dashCooldown - _currentDashCooldown)/_dashLength);

            if (_dashCooldown - _currentDashCooldown >= _dashLength)
            {
                _isDashing = false;
            }
        }
        if (_currentDashCooldown > 0)
        {
            _currentDashCooldown -= Time.deltaTime;
            if (_currentDashCooldown <= 0)
            {
                _isDashing = false;
            }
        }
        
        // Not moving
        _animator.SetFloat(_horizontal, _movement.x);
        _animator.SetFloat(_vertical, _movement.y);
        if (_movement != Vector2.zero)
        {
            _lastDirection = _movement;
            _animator.SetFloat(_lastHorizontal, _movement.x);
            _animator.SetFloat(_lastVertical, _movement.y);
        }
    }
}
