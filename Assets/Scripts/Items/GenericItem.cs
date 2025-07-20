using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class GenericItem : MonoBehaviour, IGrabbable
{
    [SerializeField] private float _delayToEnableColliderWhenThrow = .2f;
    private Rigidbody _rigidbody;
    private Collider _collider;
    public bool CanGrab { get; set; }
    [field: SerializeField] public GrabbableType HoldType { get; set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        CanGrab = true;
    }

    public void Grab(Transform parent, Vector3 position, bool local)
    {
        if (!CanGrab)
            return;

        transform.SetParent(parent);
        if (local)
        {
            transform.localPosition = position;
        }
        else
        {
            transform.position = position;
        }
        _rigidbody.isKinematic = true;
    }

    public async void Throw(Vector3 direction, float force)
    {
        _collider.enabled = false;
        Release();
        _rigidbody.AddForce(direction.normalized * force);
        await Awaitable.WaitForSecondsAsync(_delayToEnableColliderWhenThrow);
        _collider.enabled = true;
    }

    public void Release()
    {
        _rigidbody.isKinematic = false;
        transform.SetParent(null);
    }
}
