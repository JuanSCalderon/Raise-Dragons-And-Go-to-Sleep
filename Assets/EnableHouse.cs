using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableHouse : MonoBehaviour
{
 [SerializeField] private DragonAlimentationController dragonAlimentationController;
    [SerializeField] private SleepController sleepController;
    [SerializeField] private GameObject enableHouseYoungDragon; // Referencia al GameObject que se activará

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
                StartCoroutine(ActivateYoungDragonHouseAfterDelay(15f));
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

    private IEnumerator ActivateYoungDragonHouseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo especificado
        if (sleepController.IsSleepingCount == 1 && dragonAlimentationController.ComidaBabyDragonCount >= 5)
        {
            // Activa el GameObject después del retraso
            Debug.Log("Activando EnableHouseYoungDragon.");
            enableHouseYoungDragon.SetActive(true);
        }
    }
}