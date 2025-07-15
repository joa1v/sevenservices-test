using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    [SerializeField] private Transform _targetLimb;
    [SerializeField] private bool _mirror;
    [SerializeField] private Vector3 _eulersOffset;
    private ConfigurableJoint _joint;

    private void Start()
    {
        _joint = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        var quartOffset = Quaternion.Euler(_eulersOffset);
        _joint.targetRotation = _mirror ? Quaternion.Inverse(_targetLimb.rotation) : _targetLimb.rotation * quartOffset;
    }
}
