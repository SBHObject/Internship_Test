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

    private readonly Quaternion flip = new Quaternion(0, 180, 0, 0);
    private readonly Quaternion NonFlip = new Quaternion(0, 0, 0, 0);

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
            sprite.gameObject.transform.rotation = flip;
            IsFlip = true;
        }
        else if(value.x > 0)
        {
            sprite.gameObject.transform.rotation = NonFlip;
            IsFlip = false;
        }
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * character.StatData.MoveSpeed;

        rb.velocity = direction;
    }
}
