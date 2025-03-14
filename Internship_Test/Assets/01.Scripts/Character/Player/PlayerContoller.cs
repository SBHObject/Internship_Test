using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    private PlayerCharacter character;
    private Rigidbody2D rb;

    private Vector2 moveDir = Vector2.zero;

    [SerializeField]
    private SpriteRenderer sprite;
    public bool IsFlip { get; private set; } = false;

    private void Awake()
    {
        character = GetComponent<PlayerCharacter>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        character.InputController.OnMoveInput += Move;
    }

    private void FixedUpdate()
    {
        ApplyMovement(moveDir);
    }

    private void Move(Vector2 value)
    {
        moveDir = value;

        if(value.x < 0)
        {
            sprite.flipX = true;
            IsFlip = true;
        }
        else
        {
            sprite.flipX = false;
            IsFlip = false;
        }
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * character.StatData.MoveSpeed;

        rb.velocity = direction;
    }
}
