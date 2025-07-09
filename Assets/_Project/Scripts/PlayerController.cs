using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _rotationSpeed = 10f;
    private Vector3 _direction;

    [SerializeField] private float _jumpForce = 6f;
    [SerializeField] private int _maxJumps = 2;
    [SerializeField] private float _sprintMultiplier = 2f;

    private GroundCheck _groundCheck;
    private int _currentJumps;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _groundCheck = GetComponent<GroundCheck>();
        _currentJumps = _maxJumps;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        _direction = new Vector3(h, 0f, v).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && _currentJumps > 0)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _currentJumps--;
        }

    }

    private void FixedUpdate()
    {
        float currentSpeed = _speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= _sprintMultiplier;
        }
        Vector3 targetPosition = _rb.position + _direction * currentSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(targetPosition);

        if (_direction.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_direction);
            Quaternion newRotation = Quaternion.Slerp(_rb.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
            _rb.MoveRotation(newRotation);
        }

        if (_groundCheck.IsGrounded())
        {
            _currentJumps = _maxJumps;
        }        

    }
}
