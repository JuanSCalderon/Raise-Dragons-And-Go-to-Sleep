using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepController : MonoBehaviour
{
    public bool isSleep;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSleep && Input.GetKeyDown(KeyCode.G)){ 
            
            Debug.Log("El personaje fue a dormir");
            GameObject player = GameObject.FindGameObjectWithTag("Player"); // Encuentra el jugador
            if (player != null)
            {
                player.SetActive(false); // Desactiva el jugador
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Verifica si el objeto que colisiona es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("El personaje quiere entrar a la casa");
            isSleep = false; // Marca que el jugador está dentro de la casa
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // Si el jugador sale de la colisión, marca que ya no está en la casa
        if (other.gameObject.CompareTag("Player"))
        {
            isSleep = true;
        }
    }
}
