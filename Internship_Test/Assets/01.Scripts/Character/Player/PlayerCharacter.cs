using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IDamageable
{
    //플레이어 스텟 관련 필드
    [SerializeField]
    private PlayerStatSO statData;
    public PlayerStatSO StatData { get { return statData; } }

    //실제 스텟 관리 클래스
    public PlayerStatus Status { get; private set; }

    //컨트롤 관련 필드
    public PlayerContoller Controller { get; private set; }
    public InputController InputController { get; private set; }

    public bool IsAlive { get; private set; } = true;

    public event Action OnPlayerDeath;

    private void Awake()
    {
        Controller = GetComponent<PlayerContoller>();
        InputController = GetComponent<InputController>();

        Status = new PlayerStatus(this);
    }

    public void TakeDamage(float damage)
    {
        if (Status.TakeDamage(damage) <= 0)
        {
            PlayerDeath();
        }
    }

    //플레이어 사망
    private void PlayerDeath()
    {
        IsAlive = false;
        OnPlayerDeath?.Invoke();
    }

    public void TakeHeal(float amount)
    {
        Status.TakeHeal(amount);
    }
}
