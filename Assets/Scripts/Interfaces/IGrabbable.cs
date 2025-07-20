using UnityEngine;

public interface IGrabbable
{
    public bool CanGrab { get; set; }
    public GrabbableType HoldType { get; set; }

    void Grab(Transform parent, Vector3 position, bool local);
    void Throw(Vector3 direction, float force);
    void Release();
}
