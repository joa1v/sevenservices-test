using UnityEngine;

public class RagdollSetter : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _ragdollRoot;
    [SerializeField] private Collider _collider;

    private Rigidbody[] _rigidbodies;
    private CharacterJoint[] _characterJoints;
    private Collider[] _colliders;

    public Rigidbody[] Rigidbodies => _rigidbodies;
    public CharacterJoint[] CharacterJoints => _characterJoints;
    public Collider[] Colliders => _colliders;

    private void Awake()
    {
        _rigidbodies = _ragdollRoot.GetComponentsInChildren<Rigidbody>();
        _characterJoints = _ragdollRoot.GetComponentsInChildren<CharacterJoint>();
        _colliders = _ragdollRoot.GetComponentsInChildren<Collider>();

        DisableRagdoll();
    }

    [ContextMenu("Enable")]
    public void EnableRagdoll()
    {
        SetRagdoll(true);
    }

    [ContextMenu("Disable")]
    public void DisableRagdoll()
    {
        SetRagdoll(false);
    }

    protected virtual void SetRagdoll(bool set)
    {
        _animator.enabled = !set;
        _collider.enabled = !set;
        foreach (var rb in _rigidbodies)
        {
            rb.detectCollisions = set;
            rb.isKinematic = !set;
            rb.angularVelocity = Vector3.zero;
            rb.linearVelocity = Vector3.zero;
        }

        foreach (var joint in _characterJoints)
        {
            joint.enableCollision = set;
        }

        foreach (var col in _colliders)
        {
            col.enabled = set;
        }
    }

    public void SetColliders(bool set)
    {
        foreach (var col in _colliders)
        {
            col.enabled = set;
        }
    }

    public void SetRigidbodies(bool set)
    {
        foreach (var rb in _rigidbodies)
        {
            rb.detectCollisions = set;
            rb.isKinematic = !set;
            rb.angularVelocity = Vector3.zero;
            rb.linearVelocity = Vector3.zero;
        }

    }
}
