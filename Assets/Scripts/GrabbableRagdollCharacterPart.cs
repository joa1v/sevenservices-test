using UnityEngine;

public class GrabbableRagdollCharacterPart : MonoBehaviour, IGrabbable
{
    public bool CanGrab { get; set; } = true;
    public RagdollCharacter RagDollChar { get; set; }
    [field: SerializeField] public GrabbableType HoldType { get; set; }

    private ConfigurableJoint _joint;

    public void Grab(Transform hand, Vector3 positions, bool local)
    {
        if (!CanGrab || _joint != null)
            return;

        RagDollChar.Grab();
        Rigidbody targetRb = GetComponent<Rigidbody>();
        Rigidbody handRb = hand.GetComponent<Rigidbody>();

        if (handRb == null)
        {
            handRb = hand.gameObject.AddComponent<Rigidbody>();
            handRb.isKinematic = true;
        }

        _joint = hand.gameObject.AddComponent<ConfigurableJoint>();
        _joint.connectedBody = targetRb;
        _joint.autoConfigureConnectedAnchor = false;
        _joint.anchor = Vector3.zero;
        _joint.connectedAnchor = Vector3.zero;

        _joint.xMotion = ConfigurableJointMotion.Locked;
        _joint.yMotion = ConfigurableJointMotion.Locked;
        _joint.zMotion = ConfigurableJointMotion.Locked;

        _joint.angularXMotion = ConfigurableJointMotion.Limited;
        _joint.angularYMotion = ConfigurableJointMotion.Limited;
        _joint.angularZMotion = ConfigurableJointMotion.Limited;

        JointDrive drive = new JointDrive
        {
            positionSpring = 8000f,
            positionDamper = 200f,
            maximumForce = Mathf.Infinity
        };

        _joint.slerpDrive = drive;
        _joint.rotationDriveMode = RotationDriveMode.Slerp;
    }

    public void Throw(Vector3 direction, float force)
    {
        RagDollChar.Throw();
        Release();

        var rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(direction.normalized * force);
    }

    public void Release()
    {
        if (_joint != null)
        {
            Destroy(_joint);
            _joint = null;
        }
    }
}
