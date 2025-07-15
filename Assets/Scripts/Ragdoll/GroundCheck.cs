using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private PlayerRagdollController _ragdoll;
    private void OnTriggerEnter(Collider other)
    {
        _ragdoll.SetIsOnGround();
    }
}
