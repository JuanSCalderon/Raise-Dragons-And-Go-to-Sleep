using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableHouse : MonoBehaviour
{
 [SerializeField] private DragonAlimentationController dragonAlimentationController;
    [SerializeField] private SleepController sleepController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (sleepController.IsSleepingCount == 0 && dragonAlimentationController.ComidaBabyDragonCount >= 5)
            {
                // Desactiva el objeto para permitir pasar
                Debug.Log("El BabyDragon ha sido alimentado lo suficiente. Puedes entrar a la casa.");
                gameObject.SetActive(false);
                 // Llama a la coroutine
            }

            else if (sleepController.IsSleepingCount == 1 && dragonAlimentationController.ComidaYoungDragonCount >= 10)
            {
                // Reactiva el objeto después de dormir y el joven dragón ha sido alimentado
                Debug.Log("El YoungDragon ha sido alimentado lo suficiente. Puedes entrar a la casa nuevamente.");
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("El jugador tiene tareas pendientes.");
            }
        }
    }


}