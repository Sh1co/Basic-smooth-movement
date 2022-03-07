using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private float _movementSpeed = 5.0f;
    [SerializeField] private float _acceleration = 7.0f;
    [SerializeField] private float _breaking = 15.0f;
    [SerializeField] [Range(0.01f, 1.0f)] private float _rotationSpeed = 0.15f;
    
    void Update()
    {
        _moveHorizontal = Input.GetAxisRaw ("Horizontal");
        _moveVertical = Input.GetAxisRaw ("Vertical");

        if (_moveHorizontal != 0 || _moveVertical != 0)
        {
            _movementDir = new Vector3(_moveHorizontal, 0.0f, _moveVertical);
            _movementDir = _movementDir.normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_movementDir), _rotationSpeed);
            _velocity += _acceleration * Time.deltaTime;
            _velocity = Mathf.Min(_movementSpeed, _velocity);
        }
        else
        {
            _velocity -= _breaking * Time.deltaTime;
            _velocity = Mathf.Max(0f, _velocity);
        }
        
        transform.Translate(_movementDir * (_velocity * Time.deltaTime), Space.World);
    }

    private float _velocity = 0;
    private float _moveHorizontal;
    private float _moveVertical;
    private Vector3 _movementDir;

}
