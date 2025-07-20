using UnityEngine;

public class RagdollCharacterAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private RagdollCharacter _ragdollChar;
    [SerializeField] private string _hitParamenter;

    private void Start()
    {
        _ragdollChar.OnDamageTaken += PlayHitAnim;
    }

    private void PlayHitAnim()
    {
        _animator.SetTrigger(_hitParamenter);
    }
}
