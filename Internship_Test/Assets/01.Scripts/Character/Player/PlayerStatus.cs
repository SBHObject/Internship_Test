using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    private PlayerCharacter character;

    private float maxHealth;
    public float MaxHealth { get { return maxHealth; } }
    public float CurrentHealth {  get; private set; }

    public int Level { get; private set; }
    public float CurrentExp { get; private set; }
    private float needExp;
    public float NeedExp { get { return needExp; } }

    public event Action OnHpChange;
    public event Action OnExpChange;

    public PlayerStatus(PlayerCharacter player)
    {
        character = player;

        maxHealth = character.StatData.MaxHP;
        CurrentHealth = maxHealth;

        Level = 1;
        needExp = Level * character.StatData.NeedExpPerLevel;
    }

    public void GetExp(float getExp)
    {
        CurrentExp += getExp;

        while (CurrentExp > needExp)
        {
            LevelUp();
        }

        OnExpChange?.Invoke();
    }

    private void LevelUp()
    {
        CurrentExp -= needExp;
        Level++;
        needExp = character.StatData.NeedExpPerLevel * Level;

        OnExpChange?.Invoke();
    }

    public float TakeDamage(float damage)
    {
        CurrentHealth = (CurrentHealth <= damage) ? 0 : CurrentHealth - damage;
        OnHpChange?.Invoke();
        return CurrentHealth;
    }

    public void TakeHeal(float heal)
    {
        CurrentHealth = (CurrentHealth + heal >= maxHealth) ? maxHealth : CurrentHealth + heal;
        OnHpChange?.Invoke();
    }
}
