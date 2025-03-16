using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerCharacter character;

    private readonly int isDeadHesh = Animator.StringToHash("IsDead");
    private readonly int isMoveHesh = Animator.StringToHash("IsMove");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        character = GetComponent<PlayerCharacter>();
    }

    private void Start()
    {
        character.OnPlayerDeath += PlayerDeath;
        character.InputController.OnMoveInput += PlayerMove;
    }

    private void PlayerDeath()
    {
        animator.SetBool(isDeadHesh, true);
    }

    private void PlayerMove(Vector2 value)
    {
        if(value.magnitude == 0)
        {
            animator.SetBool(isMoveHesh, false);
        }
        else
        {
            animator.SetBool(isMoveHesh, true);
        }
    }
}
