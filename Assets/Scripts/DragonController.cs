using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public enum DragonState { Baby, Adolescent, Adult }

    [SerializeField] private DragonState currentState;
    [SerializeField] private Animator animator;
    private float speed = 1f;
    private float xRangeMin = -1f;
    private float xRangeMax = 5f;
    private float yRangeMin = -1f;
    private float yRangeMax = -2.12f;
    private Vector3 initialPosition;

    private float time;

    void Start()
    {
        initialPosition = gameObject.transform.position; //posición inicial del dragón
        ChangeAnimation(currentState);
    }

    void Update()
    {
        time += Time.deltaTime * speed;
        float x = Mathf.Lerp(xRangeMin, xRangeMax, (Mathf.Sin(time) + 1f) / 2f);
        float y = Mathf.Lerp(yRangeMin, yRangeMax, (Mathf.Cos(time) + 1f) / 2f);
        transform.position = initialPosition + new Vector3(x, y, 0);

        // Puedes agregar lógica adicional aquí para cambiar el estado del dragón y las animaciones
    }

    void ChangeAnimation(DragonState state)
    {
        switch (state)
        {
            case DragonState.Baby:
                animator.Play("VFXBabyDragon");
                break;
            case DragonState.Adolescent:
                animator.Play("VFXYoungDragon");
                break;
            case DragonState.Adult:
                animator.Play("VFXAdultDragon_flying");
                break;
                // Decide cuál animación reproducir
                // if (/* condición para volar */)
                // {
                //     animator.Play("VFXAdultDragon_flying");
                // }
                // else
                // {
                //     animator.Play("VFXAdultDragon_eating");
                // }
                // break;
        }
    }

    // Este método podría ser llamado para cambiar el estado del dragón y la animación
    public void SetDragonState(DragonState newState)
    {
        currentState = newState;
        ChangeAnimation(newState);
    }
}
