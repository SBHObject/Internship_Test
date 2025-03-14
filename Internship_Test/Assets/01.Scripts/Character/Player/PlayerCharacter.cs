using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IDamageable
{
    //�÷��̾� ���� ���� �ʵ�
    [SerializeField]
    private PlayerStatSO statData;
    public PlayerStatSO StatData { get { return statData; } }

    //���� ���� ���� Ŭ����
    public PlayerStatus Status { get; private set; }

    //��Ʈ�� ���� �ʵ�
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

    //�÷��̾� ���
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
