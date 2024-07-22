using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayerController : MonoBehaviour
{
    public float speed;
    private Vector2 moveAxis;
    private Vector2 moveDir;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Capturar entrada del jugador en los ejes horizontales y verticales
        moveAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        // Normalizar el vector de movimiento si su magnitud es mayor que 1
        if (moveAxis.magnitude > 1)
        {
            moveAxis.Normalize();
        }
        
        // Multiplicar por la velocidad
        moveDir = moveAxis * speed;
    }

    void FixedUpdate()
    {
        // Mover el objeto usando el Rigidbody2D para manejar la física
        rb.velocity = moveDir * Time.fixedDeltaTime;
    }
}


