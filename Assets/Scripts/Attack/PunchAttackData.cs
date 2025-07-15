using UnityEngine;

[CreateAssetMenu(menuName = "Attack/PunchAttackData")]
public class PunchAttackData : AttackData
{
    [SerializeField] private LayerMask _targetLayer;

    public override void ExecuteAttack(Transform origin, GameObject attacker)
    {
        Collider[] hits = Physics.OverlapSphere(origin.position, _attackRange, _targetLayer);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IDamageable damageable))
            {
                Vector3 hitPoint = hit.ClosestPoint(origin.position);
                Vector3 hitDirection = hitPoint - origin.position;

                if (hitDirection == Vector3.zero)
                    hitDirection = origin.forward; 

                hitDirection.Normalize();
                damageable.TakeDamage(_damage, hitPoint, hitDirection);
            }
        }
    }
}
