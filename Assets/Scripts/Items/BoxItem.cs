using System.Threading.Tasks;
using UnityEngine;

public class BoxItem : MonoBehaviour, IGrabbable
{
    [SerializeField] private float _delayToEnableColliderWhenThrow;
    private Rigidbody _rigidbody;
    private Collider _collider;
    public bool CanGrab { get; set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        CanGrab = true;
    }

    public void Grab(Transform parent)
    {
        if (!CanGrab)
            return;

        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
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
