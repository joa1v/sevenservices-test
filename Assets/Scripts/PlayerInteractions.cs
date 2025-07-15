using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private float _tapThreshold = 0.2f;

    [Header("Grab")]
    [SerializeField] private Vector3 _castOffset;
    [SerializeField] private float _grabRadius;
    [SerializeField] private float _throwForce = 100;

    [Header("Attack")]
    [SerializeField] private CharacterAttack _attack;
    [SerializeField] private AttackData _leftAttackData;
    [SerializeField] private AttackData _rightAttackData;
    [Header("Animation")]

    [SerializeField] private Animator _animator;
    [SerializeField] private string _leftThrowTrigger = "LeftThrow";
    [SerializeField] private string _rightThrowTrigger = "RightThrow";

    private IGrabbable _grabbable;
    private float _leftPressTime;
    private float _rightPressTime;
    private bool _isGrabbingLeft;
    private bool _isGrabbingRight;

    public void OnLeftPunchGrab(InputValue value)
    {
        if (value.isPressed)
        {
            _leftPressTime = Time.time;
        }
        else
        {
            float heldTime = Time.time - _leftPressTime;
            if (heldTime > _tapThreshold)
            {
                if (!_isGrabbingLeft)
                {
                    TryGrab(_leftHand);
                }
                else
                {
                    PlayLefThrowAnim();
                }
            }
            else
            {
                LeftAttack();
            }
        }
    }

    public void OnRightPunchGrab(InputValue value)
    {
        if (value.isPressed)
        {
            _rightPressTime = Time.time;
        }
        else
        {
            float heldTime = Time.time - _rightPressTime;
            if (heldTime > _tapThreshold)
            {
                if (!_isGrabbingRight)
                {
                    TryGrab(_rightHand, false);
                }
                else
                {
                    PlayRightThrowAnim();
                }
            }
            else
            {
                RightAttack();
            }
        }
    }

    private void TryGrab(Transform hand, bool left = true)
    {
        Vector3 position = left ? hand.position - _castOffset : hand.position + _castOffset;
        Collider[] hits = Physics.OverlapSphere(position, _grabRadius);
        foreach (var hit in hits)
        {
            if (hit.attachedRigidbody != null && hit.attachedRigidbody != GetComponent<Rigidbody>())
            {
                if (!hit.TryGetComponent<IGrabbable>(out var grabbable)) continue;
                _grabbable = grabbable;
                _grabbable.Grab(hand);
                if (left)
                {
                    _isGrabbingLeft = true;
                }
                else
                {
                    _isGrabbingRight = true;
                }
                break;
            }
        }
    }

    private void PlayLefThrowAnim()
    {
        _animator.SetTrigger(_leftThrowTrigger);
    }

    private void PlayRightThrowAnim()
    {
        _animator.SetTrigger(_rightThrowTrigger);
    }

    public void Throw()
    {
        if (_isGrabbingLeft)
        {
            LeftThrow();
        }
        else
        {
            RightThrow();
        }
    }

    private void LeftThrow()
    {
        _grabbable.Throw(transform.forward, _throwForce);
        _isGrabbingLeft = false;
    }

    private void RightThrow()
    {
        _grabbable.Throw(transform.forward, _throwForce);
        _isGrabbingRight = false;
    }

    private void LeftAttack()
    {
        _attack.SetAttack(_leftAttackData, _leftHand);
        _attack.Attack();
    }

    private void RightAttack()
    {
        _attack.SetAttack(_rightAttackData, _rightHand);
        _attack.Attack();
    }

    private void OnDrawGizmosSelected()
    {
        if (_leftHand != null)
            Gizmos.DrawWireSphere(_leftHand.position - _castOffset, _grabRadius);
        if (_rightHand != null)
            Gizmos.DrawWireSphere(_rightHand.position + _castOffset, _grabRadius);
    }
}

