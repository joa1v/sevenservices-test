using UnityEngine;

public abstract class AttackData : ScriptableObject
{
    [SerializeField] protected string _animationTrigger;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _attackCooldown;

    public string AnimationTrigger => _animationTrigger;
    public float AttackRange => _attackRange;
    public float Damage => _damage;
    public float AttackCooldown => _attackCooldown;

    public abstract void ExecuteAttack(Transform origin, GameObject attacker);
}
