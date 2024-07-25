using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    [SerializeField] private DragonAlimentationController dragonAlimentationController;
    public float speed;
    private Vector2 moveAxis;
    private Vector2 moveDir;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public event Action OnEncountered;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
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

        // Comprobar si el dragón adulto ha recibido 20 comidas
        CheckDragonFoodCount();
    }

    void FixedUpdate()
    {
        rb.velocity = moveDir * Time.fixedDeltaTime;
    }

    void CheckDragonFoodCount()
    {
        if (dragonAlimentationController.ComidaAdultDragonCount == 20)
        {
            ActivationBattle();
        }
    }

    void ActivationBattle()
    {
        animator.SetBool("walking", false);
        OnEncountered?.Invoke();
    }

    public void PickUpVegetables()
    {
        animator.SetBool("carrying", true);
        StartCoroutine(DisableCarryingAnimation());
    }

    private IEnumerator DisableCarryingAnimation()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("carrying", false);
    }
}