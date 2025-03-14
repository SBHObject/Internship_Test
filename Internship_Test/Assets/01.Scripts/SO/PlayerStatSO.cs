using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "SO/PlayerSO")]
public class PlayerStatSO : ScriptableObject
{
    [Header("Move")]
    public float MoveSpeed;

    [Header("Combat")]
    public float MaxHP;
    public float InvincibleTime;
}
