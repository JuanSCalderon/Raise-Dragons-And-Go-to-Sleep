using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepController : MonoBehaviour
{
    private bool playerInHouse = false;
    private SpriteRenderer playerRenderer;
    private PlayerController1 playerMovement;
    public ParticleSystem particulas;
    public int isSleepingCount=0;
    private ControladorCicloDia cicloNocheScript; 
    [SerializeField] private GameObject controladorCicloNoche; // Referencia al GameObject del ControladorCicloNoche
    [SerializeField] private DragonAlimentationController dragonAlimentationController;
    public delegate void OnPlayerSleep();
    public event OnPlayerSleep OnPlayerSleepEvent;


    
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerRenderer = player.GetComponent<SpriteRenderer>(); // Obtiene el SpriteRenderer del jugador
            playerMovement = player.GetComponent<PlayerController1>();        
        }
        if (controladorCicloNoche != null)
        {
            cicloNocheScript = controladorCicloNoche.GetComponent<ControladorCicloDia>();
            controladorCicloNoche.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInHouse && Input.GetKeyDown(KeyCode.G))
        {

            particulas.Play();
            Debug.Log("El personaje fue a dormir");
            MakePlayerTransparent();
        if (playerMovement != null)
            {
                playerMovement.enabled = false; // Desactiva el movimiento del jugador
            }
        isSleepingCount++;
        Debug.Log("La cuenta de veces dormidas es "+ isSleepingCount);

        ActivateControladorCicloNoche();
        StartCoroutine(WakeUpPlayer());

        OnPlayerSleepEvent?.Invoke();

        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Verifica si el objeto que colisiona es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Press G to sleep");
            playerInHouse = true; 
        }
    }


    private void MakePlayerTransparent()
    {
        if (playerRenderer != null)
        {
            // Cambia el color del jugador para hacerlo transparente
            Color newColor = playerRenderer.color;
            newColor.a = 0f; // Establece la transparencia a 0 (totalmente transparente)
            playerRenderer.color = newColor;
        }
    }

    private void ResetPlayerTransparency()
    {
        if (playerRenderer != null)
        {
            // Restablece el color original del jugador
            Color originalColor = playerRenderer.color;
            originalColor.a = 1f; // Establece la transparencia a 1 (totalmente opaco)
            playerRenderer.color = originalColor;
        }
    }
    private IEnumerator WakeUpPlayer()
    {
        // Espera 16 segundos antes de "despertar" al jugador
        yield return new WaitForSeconds(15f);
        playerMovement.enabled = true;
        Debug.Log("El personaje ha despertado.");
        ResetPlayerTransparency();
        particulas.Stop();  
        if (controladorCicloNoche != null)
        {
            controladorCicloNoche.SetActive(false);
        }
    }
    private void ActivateControladorCicloNoche()
    {
        // Activa el ControladorCicloNoche cuando el jugador duerme
        if (controladorCicloNoche != null)
        {
            controladorCicloNoche.SetActive(true);
            cicloNocheScript.ReiniciarCiclo();    
        }
        
    }
    public int IsSleepingCount => isSleepingCount; // Propiedad para acceder al conteo de sueños

}
