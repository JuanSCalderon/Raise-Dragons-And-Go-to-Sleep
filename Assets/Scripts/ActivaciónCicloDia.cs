using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivaciónCicloDia : MonoBehaviour
{
    [SerializeField] private Recolection recolectados;
    [SerializeField] private GameObject controladorCicloDia;

    void Start()
    {
                if (controladorCicloDia != null)
        {
            controladorCicloDia.SetActive(false);
        } 
    }

    void Update()
    {
         if (recolectados.RemolachaCount + recolectados.ColiflorCount == 10)
        {
            // Activa el controlador del ciclo del día si aún no está activo.
            if (!controladorCicloDia.activeSelf)
            {
                controladorCicloDia.SetActive(true);
                Debug.Log("Controlador del ciclo del día activado.");
            }
        }
    }
}
