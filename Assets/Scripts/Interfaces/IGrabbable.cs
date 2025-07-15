using UnityEngine;

public interface IGrabbable
{
    public bool CanGrab { get; set; }

    void Grab(Transform parent);
    void Throw(Vector3 direction, float force);
    void Release();
}
