using UnityEngine;

public class PlayerRagdollSetter : RagdollSetter
{
    [SerializeField] private CharacterController _charController;

    protected override void SetRagdoll(bool set)
    {
        _charController.detectCollisions = !set;

        base.SetRagdoll(set);
    }
}
