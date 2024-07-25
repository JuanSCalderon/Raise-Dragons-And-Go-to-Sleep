using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ControladorCicloDiaYoungDragon : MonoBehaviour
{
[SerializeField] private Light2D luzglobal;
    [SerializeField] private cicloDia[] ciclosDiaYoungDragon;
    [SerializeField] private float tiempoPorCiclo;
    private float tiempoActualCiclo = 0;
    private float porcentajeCiclo;
    private int cicloActual = 0;
    private int cicloSiguiente = 1;
    public bool cicloCompletado = false;

    public delegate void OnCicloCompletado();
    public event OnCicloCompletado CicloCompletado;

    private void Start() {
        if (ciclosDiaYoungDragon.Length > 0) {
            luzglobal.color = ciclosDiaYoungDragon[0].colorCiclo;
        } else {
            Debug.LogError("El array ciclosDiaYoungDragon está vacío. Asegúrate de asignar elementos en el Inspector.");
        }
    }

    private void Update() {
        if (!cicloCompletado) {
            tiempoActualCiclo += Time.deltaTime;
            porcentajeCiclo = tiempoActualCiclo / tiempoPorCiclo;

            if (tiempoActualCiclo >= tiempoPorCiclo) {
                tiempoActualCiclo = 0;
                cicloActual = cicloSiguiente;
                if (cicloSiguiente + 1 > ciclosDiaYoungDragon.Length - 1) {
                    cicloCompletado = true;
                    Debug.Log("Ciclo completado para YoungDragon: se ha alcanzado la última fase.");
                    CicloCompletado?.Invoke();
                } else {
                    cicloSiguiente += 1;
                }
            }
            CambiarColor(ciclosDiaYoungDragon[cicloActual].colorCiclo, ciclosDiaYoungDragon[cicloSiguiente].colorCiclo);
        }
    }

    private void CambiarColor(Color colorActual, Color siguienteColor) {
        luzglobal.color = Color.Lerp(colorActual, siguienteColor, porcentajeCiclo);
    }

    public void ReiniciarCiclo() {
        cicloActual = 0;
        cicloSiguiente = 1;
        cicloCompletado = false;
        tiempoActualCiclo = 0;
        luzglobal.color = ciclosDiaYoungDragon[0].colorCiclo;
        Debug.Log("Ciclo reiniciado para YoungDragon.");
    }
}