using System;
using UnityEngine;

public class RagdollCharacter : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _forceToApplyOnDeath = 100f;
    [SerializeField] private RagdollSetter _ragdollSetter;

    private float _currentHealth;
    private bool _isDead;
    private GrabbableRagdollCharacterPart[] _parts;

    public Action OnDamageTaken { get; set; }
    public Action OnDeath { get; set; }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _ragdollSetter.DisableRagdoll();
        _parts = GetComponentsInChildren<GrabbableRagdollCharacterPart>();
        foreach (var part in _parts)
        {
            part.RagDollChar = this;
        }
    }

    public void TakeDamage(float amount, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (_isDead) return;

        Debug.Log("taking damage: " + amount);
        _currentHealth -= amount;
        OnDamageTaken?.Invoke();
        if (_currentHealth <= 0f)
        {
            Die(hitPoint, hitDirection);
        }
    }

    private void Die(Vector3 hitPoint, Vector3 hitDirection)
    {
        _isDead = true;

        _ragdollSetter.EnableRagdoll();
        SetCanGrabParts(true);

        ApplyForceToClosestRigidbody(hitPoint, hitDirection);
        OnDeath?.Invoke();
    }

    private async void ApplyForceToClosestRigidbody(Vector3 hitPoint, Vector3 hitDirection)
    {
        await Awaitable.EndOfFrameAsync();
        if (_ragdollSetter.Rigidbodies.Length == 0) return;

        Rigidbody closestRb = _ragdollSetter.Rigidbodies[0];
        float closestDistance = Vector3.Distance(closestRb.worldCenterOfMass, hitPoint);

        foreach (var rb in _ragdollSetter.Rigidbodies)
        {
            float dist = Vector3.Distance(rb.worldCenterOfMass, hitPoint);
            if (dist < closestDistance)
            {
                closestRb = rb;
                closestDistance = dist;
            }
        }

        float force = _forceToApplyOnDeath;
        closestRb.AddForceAtPosition(hitDirection.normalized * force, hitPoint, ForceMode.Impulse);
    }

    private void SetCanGrabParts(bool set)
    {
        foreach (var part in _parts)
        {
            part.CanGrab = set;
        }
    }

    public void Grab()
    {
        foreach (var col in _ragdollSetter.Colliders)
        {
            col.enabled = false;
        }
    }


    public void Throw()
    {
        foreach (var col in _ragdollSetter.Colliders)
        {
            col.enabled = true;
        }
    }

    public void Release()
    {

    }

}
