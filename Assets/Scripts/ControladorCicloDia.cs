using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ControladorCicloDia : MonoBehaviour
{
    [SerializeField] private Light2D luzglobal;
    [SerializeField] private cicloDia[] ciclosDia;
    [SerializeField] private float tiempoPorCiclo;
    private float tiempoActualCiclo = 0;
    private float porcentajeCiclo;
    private int cicloActual = 0;
    private int cicloSiguiente = 1;
    private bool cicloCompletado = false;

    private void Start() {
        luzglobal.color = ciclosDia[0].colorCiclo;
    }

    private void Update() {
        if (!cicloCompletado) {
            tiempoActualCiclo += Time.deltaTime;
            porcentajeCiclo = tiempoActualCiclo / tiempoPorCiclo;

            if (tiempoActualCiclo >= tiempoPorCiclo) {
                tiempoActualCiclo = 0;
                cicloActual = cicloSiguiente;
                if (cicloSiguiente + 1 > ciclosDia.Length - 1) {
                    cicloCompletado = true;  // Detener el ciclo al final
                    Debug.Log("Ciclo completado: se ha alcanzado la última fase.");
                } else {
                    cicloSiguiente += 1;
                }
            }
            CambiarColor(ciclosDia[cicloActual].colorCiclo, ciclosDia[cicloSiguiente].colorCiclo);
        }

        // Verifica la condición para reiniciar el ciclo
        if (Input.GetKeyDown(KeyCode.R)) {
            ReiniciarCiclo();
        }
    }

    private void CambiarColor(Color colorActual, Color siguienteColor) {
        luzglobal.color = Color.Lerp(colorActual, siguienteColor, porcentajeCiclo);
    }

    private void ReiniciarCiclo() {
        cicloActual = 0;
        cicloSiguiente = 1;
        cicloCompletado = false;
        tiempoActualCiclo = 0;
        luzglobal.color = ciclosDia[0].colorCiclo;
        Debug.Log("Ciclo reiniciado.");
    }
}
