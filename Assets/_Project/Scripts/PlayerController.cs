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



    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        _direction = new Vector3(h, 0f, v).normalized;
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = _rb.position + _direction * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(targetPosition);

        if (_direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_direction);
            Quaternion newRotation = Quaternion.Slerp(_rb.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
            _rb.MoveRotation(newRotation);
        }


    }
}
