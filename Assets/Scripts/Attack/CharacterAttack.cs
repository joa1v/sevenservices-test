using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private AttackData _currentAttack;
    private Transform _attackOrigin;
    private float _cooldownTimer;

    private void Update()
    {
        if (_cooldownTimer > 0)
            _cooldownTimer -= Time.deltaTime;
    }

    public void SetAttack(AttackData newAttack, Transform attackOrigin)
    {
        if (newAttack != _currentAttack)
        {
            _cooldownTimer = 0;
        }
        _currentAttack = newAttack;
        _attackOrigin = attackOrigin;
    }

    public void ResetAttack()
    {
        _currentAttack = null;
    }

    public void Attack()
    {
        if (_cooldownTimer > 0)
            return;

        _animator.SetTrigger(_currentAttack.AnimationTrigger);
        _cooldownTimer = _currentAttack.AttackCooldown;
    }

    public void ApplyDamageFromAnimation()
    {
        _currentAttack.ExecuteAttack(_attackOrigin, gameObject);
    }
}
