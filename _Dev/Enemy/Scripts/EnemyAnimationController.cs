using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator _animator;
    private static readonly int Ready = Animator.StringToHash("Ready");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Stop = Animator.StringToHash("Stop");
    private bool _isAttacking = false;
    private static readonly int RunOffset = Animator.StringToHash("RunOffset");
    private int deathHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        EventManager.AddListener<PlayerDeathEvent>(OnPlayerDeath);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        _animator.SetFloat(RunOffset, Random.Range(0f, 1f));
        int deathType = Random.Range(1, 6);
        string deathString = "Death";
        if (deathType > 1)
        {
            deathString += deathType;
        }

        deathHash = Animator.StringToHash(deathString);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerDeathEvent>(OnPlayerDeath);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);

    }

    private void OnGameOver(GameOverEvent obj)
    {
        _animator.SetTrigger(Stop);
    }

    private void OnPlayerDeath(PlayerDeathEvent obj)
    {
        if (!_isAttacking)
            _animator.SetTrigger(Stop);
    }

    public void PlayDeathAnim()
    {
        _animator.SetTrigger(deathHash);
    }

    public void PreSwingWeapon()
    {
        _animator.SetTrigger(Ready);
    }

    public void PlayAttackAnim()
    {
        _animator.SetTrigger(Attack);
        _isAttacking = true;
    }
    
}
