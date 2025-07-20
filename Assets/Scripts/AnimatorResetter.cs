using UnityEngine;

public class AnimatorResetter : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Start()
    {
        ResetAnimator();
    }

    public void ResetAnimator()
    {
        if (_animator == null) _animator = GetComponent<Animator>();

        _animator.enabled = false;
        _animator.Rebind();
        _animator.Update(0f);
        _animator.enabled = true;
    }
}
