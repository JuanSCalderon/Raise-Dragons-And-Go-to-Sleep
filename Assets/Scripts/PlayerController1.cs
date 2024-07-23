using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public float speed;
    private Vector2 moveAxis;
    private Vector2 moveDir;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (moveAxis.magnitude > 1)
        {
            moveAxis.Normalize();
        }

        moveDir = moveAxis * speed;

        // Actualizar el Animator con el estado de caminar
        animator.SetBool("walking", moveAxis.magnitude > 0);

        // Voltear el sprite en el eje X basado en la dirección del movimiento horizontal
        if (moveAxis.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveAxis.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveDir * Time.fixedDeltaTime;
    }
}