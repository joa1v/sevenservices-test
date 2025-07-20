using System;
using UnityEngine;

public class NPCDeathChecker : MonoBehaviour
{
    [SerializeField] private RagdollCharacter _ragdollChar;
    [SerializeField] private string _defeatCollisionTag = "ArenaLimits";
    private bool _isDead;
    public Action OnDroppedOut { get; set; }


    private void Start()
    {
        _ragdollChar.OnDeath += SetDead;
    }

    private void SetDead()
    {
        _isDead = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDead && other.gameObject.tag == _defeatCollisionTag)
        {
            OnDroppedOut?.Invoke();
        }
    }
}
