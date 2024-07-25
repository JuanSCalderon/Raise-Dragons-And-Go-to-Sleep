using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DragonAlimentationController : MonoBehaviour
{
    [SerializeField] private Recolection comidaEntregada; // Referencia al script de recolección
  [SerializeField] private GameObject controladorCicloNoche; // Referencia al GameObject del ControladorCicloNoche
    [SerializeField] private SleepController sleepController;
    public Image remolachaImage; 
    public Image coliflorImage;  
    public TextMeshProUGUI remolachaCountText; 
    public TextMeshProUGUI coliflorCountText;  
    public GameObject babyDragon;
    public GameObject youngDragon;
    public GameObject adultDragon;
    
    private int remolachaDada = 0;
    private int coliflorDada = 0;

    // Contadores para cada tipo de dragón
    private int comidaDadaBabyDragon = 0;
    private int comidaDadaYoungDragon = 0;
    private int comidaDadaAdultDragon = 0;
    public int ComidaBabyDragonCount => comidaDadaBabyDragon;
    public int ComidaYoungDragonCount => comidaDadaYoungDragon;
    public int ComidaAdultDragonCount => comidaDadaAdultDragon;

    private bool isCollidingWithDragon = false;
     public int comidaParaYoungDragon = 5;
     public int comidaParaAdultDragon = 10;

    void Start()
    {
        if (comidaEntregada != null)
        {
            UpdateVegetableCountUI();
            comidaEntregada.OnVegetableCountChangedEvent += UpdateVegetableCountUI;
        }

        if (sleepController != null)
        {
            sleepController.OnPlayerSleepEvent += HandlePlayerSleep;
        }
    }

    private void OnDestroy()
    {
    {
        if (comidaEntregada != null)
        {
            comidaEntregada.OnVegetableCountChangedEvent -= UpdateVegetableCountUI;
        }

        if (sleepController != null)
        {
            sleepController.OnPlayerSleepEvent -= HandlePlayerSleep;
        }
    }
    }

     void Update()
       {
        if (isCollidingWithDragon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (comidaEntregada.ColiflorCount > 0)
                {
                    comidaEntregada.UseVegetable(Vegetable.VegetableType.Cauliflower);
                    coliflorDada++;
                    UpdateVegetableCountUI();
                    UpdateDragonSprite();
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (comidaEntregada.RemolachaCount > 0 )
                {
                    comidaEntregada.UseVegetable(Vegetable.VegetableType.Beet);
                    remolachaDada++;
                    UpdateVegetableCountUI();
                    UpdateDragonSprite();
                }
            }
        }
    }

    public void UpdateVegetableCountUI()
    {
       if (comidaEntregada != null)
        {
            remolachaCountText.text = "" + comidaEntregada.RemolachaCount;
            coliflorCountText.text = "" + comidaEntregada.ColiflorCount;
        }
    }

    private void UpdateDragonSprite()
        {
        int totalFoodGiven = remolachaDada + coliflorDada;
        int isSleepingCount = sleepController.IsSleepingCount; // Obtén el número de veces que el jugador ha dormido

        Debug.Log("Total Food Given: " + totalFoodGiven);
        Debug.Log("Comida para Young Dragon: " + comidaParaYoungDragon);
        Debug.Log("Comida para Adult Dragon: " + comidaParaAdultDragon);
                // Actualiza los contadores individuales de comida para cada tipo de dragón
        if (babyDragon.activeSelf)
        {
            comidaDadaBabyDragon = totalFoodGiven;
        }
        else if (youngDragon.activeSelf)
        {
            comidaDadaYoungDragon = totalFoodGiven;
        }
        else if (adultDragon.activeSelf)
        {
            comidaDadaAdultDragon = totalFoodGiven;
        }

        if (totalFoodGiven >= comidaParaAdultDragon && isSleepingCount >= 2)
        {
            Debug.Log("Activando AdultDragon");
            adultDragon.SetActive(true);
            youngDragon.SetActive(false);
            babyDragon.SetActive(false);
        }
        else if (totalFoodGiven >= comidaParaYoungDragon && isSleepingCount >= 1)
        {
            Debug.Log("Activando YoungDragon");
            youngDragon.SetActive(true);
            babyDragon.SetActive(false);
            adultDragon.SetActive(false);
        }
        else
        {
            Debug.Log("Activando BabyDragon");
            babyDragon.SetActive(true);
            youngDragon.SetActive(false);
            adultDragon.SetActive(false);
        }
    }
    private void HandlePlayerSleep()
    {
        // No necesitas reiniciar el contador de comida aquí, solo actualiza el sprite según el nuevo estado
        UpdateDragonSprite();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollidingWithDragon = true;
            Debug.Log("El jugador está colisionando con el dragón.");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollidingWithDragon = false;
            Debug.Log("El jugador ha dejado de colisionar con el dragón.");
        }
    }
}
