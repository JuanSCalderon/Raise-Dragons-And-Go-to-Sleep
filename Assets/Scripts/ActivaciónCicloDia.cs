using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivaciónCicloDia : MonoBehaviour
{
    [SerializeField] private GameObject controladorCicloDia;
    [SerializeField] private DragonAlimentationController dragonAlimentationController; // Referencia al controlador de alimentación
    private bool cicloDiaActivadoPorBabyDragon = false;
    private bool cicloDiaActivadoPorYoungDragon = false;

    void Start()
    {
                if (controladorCicloDia != null)
        {
            controladorCicloDia.SetActive(false);
        } 
    }

    void Update()
    {
        if (dragonAlimentationController != null)
        {
            // Verificar si el BabyDragon ha comido 5 veces
            Debug.Log("Comida BabyDragon Count: " + dragonAlimentationController.ComidaBabyDragonCount);
            if (dragonAlimentationController.ComidaBabyDragonCount >= 5 && !cicloDiaActivadoPorBabyDragon)
            {
                ActivarCicloDia();
                cicloDiaActivadoPorBabyDragon = true; // Asegúrate de que esto solo se active una vez
                Debug.Log("Ciclo del día activado por BabyDragon.");
            }

            // Verificar si el YoungDragon ha comido 10 veces
            Debug.Log("Comida YoungDragon Count: " + dragonAlimentationController.ComidaYoungDragonCount);
            if (dragonAlimentationController.ComidaYoungDragonCount >= 10 && !cicloDiaActivadoPorYoungDragon)
            {

                ActivarCicloDia();
                cicloDiaActivadoPorYoungDragon = true; // Asegúrate de que esto solo se active una vez
                Debug.Log("Ciclo del día activado por YoungDragon.");
            }
        }
    }

    private void ActivarCicloDia()
    {
        // Activa el controlador del ciclo del día si aún no está activo.
        if (controladorCicloDia != null && !controladorCicloDia.activeSelf)
        {
            controladorCicloDia.SetActive(true);
            Debug.Log("Controlador del ciclo del día activado.");
        }
    }
    private IEnumerator WakeUpPlayer()
    {
        // Espera 16 segundos antes de "despertar" al jugador
        yield return new WaitForSeconds(15f);
                if (controladorCicloDia != null)
            {
            controladorCicloDia.SetActive(false);
            }
    }
}
