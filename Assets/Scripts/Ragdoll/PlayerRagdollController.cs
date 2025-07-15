using StarterAssets;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerRagdollController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private Rigidbody _hips;
    [SerializeField] private StarterAssetsInputs _input;

    [SerializeField] private bool _isGrounded = true;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        CheckJump();
        Move();
    }

    private void Move()
    {
        float targetSpeed = _input.sprint ? _sprintSpeed : _moveSpeed;

        if (_input.move == Vector2.zero) targetSpeed = 0.0f;

        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        _hips.AddForce(transform.InverseTransformDirection(inputDirection) * targetSpeed);
    }

    private void CheckJump()
    {
        if (_isGrounded)
        {
            if (_input.jump)
            {
                _hips.AddForce(Vector3.up * _jumpForce);
                _isGrounded = false;
            }

            _input.jump = false;
        }
    }

    public void SetIsOnGround()
    {
        _isGrounded = true;
    }
}
