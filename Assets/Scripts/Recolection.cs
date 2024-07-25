using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolection : MonoBehaviour
{
    private int coliflor = 0;
    private int remolacha = 0;
    public int RemolachaCount => remolacha;
    public int ColiflorCount => coliflor;
    private Vegetable currentVegetable = null;
    private float delay;
    private bool isInTierraZone = false;
    [SerializeField] private ParticleSystem particulas;

    // Lista para mantener referencias a los vegetales
    private List<GameObject> vegetableList = new List<GameObject>();
    public delegate void OnVegetableCountChanged();
    public event OnVegetableCountChanged OnVegetableCountChangedEvent;

    private void Start()
    {
        // Encuentra y agrega todos los vegetales en la escena al inicio
        AddAllVegetablesToList(); 
    }

    private void Update()
    {
        if (currentVegetable != null && Input.GetKeyDown(KeyCode.R))
        {
            CollectVegetable(currentVegetable.vegetableType);
            currentVegetable.gameObject.SetActive(false);
            currentVegetable = null;
        }

        if (isInTierraZone && Input.GetKeyDown(KeyCode.Space))
        {
            bool hasInactiveVegetables = vegetableList.Exists(veg => !veg.activeSelf);
            if (hasInactiveVegetables){
            UpdateRandomDelay();
            StartCoroutine(ActivateVegetablesAfterDelay());
            particulas.Play();
            Debug.Log("Estás en la zona de tierra y se activarán los vegetales de 1 a 2 minutos.");
            }
        }
    }
    private void UpdateRandomDelay()
    {
        // Establece delay con un nuevo valor aleatorio
        delay = UnityEngine.Random.Range(10f, 10f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Vegetable"))
        {
            currentVegetable = other.gameObject.GetComponent<Vegetable>();
        }
        else if (other.gameObject.CompareTag("Tierra"))
        {
            isInTierraZone = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Vegetable"))
        {
            currentVegetable = null;
        }
        else if (other.gameObject.CompareTag("Tierra"))
        {
            isInTierraZone = false;
        }
    }

    private IEnumerator ActivateVegetablesAfterDelay()
    {
        // Espera el tiempo especificado en segundos
        yield return new WaitForSeconds(delay);

        // Activa todos los vegetales en la lista
        foreach (GameObject vegetable in vegetableList)
        {
            vegetable.SetActive(true);
            Debug.Log("Los vegetales se cultivaron en "+ delay+ " segundos.");
            particulas.Stop();

        }
        Debug.Log("Todos los vegetales han sido activados.");
    }

    private void AddAllVegetablesToList()
    {
        // Encuentra todos los objetos con la etiqueta "Vegetable" y agrégales a la lista
        GameObject[] vegetables = GameObject.FindGameObjectsWithTag("Vegetable");
        vegetableList.Clear();
        vegetableList.AddRange(vegetables);
    }


    public void CollectVegetable(Vegetable.VegetableType vegetableType){
        float randomValue = UnityEngine.Random.value;

        // 30% de probabilidad para entregar el vegetal
        if (randomValue > 0.40f)
        {
            switch (vegetableType)
            {
                case Vegetable.VegetableType.Beet:
                    remolacha++;
                    break;
                case Vegetable.VegetableType.Cauliflower:
                    coliflor++;
                    break;
            }
            // Agregar aqui el código para la interfaz de usuario
            Debug.Log("Beets: " + remolacha + ", Cauliflowers: " + coliflor);
            OnVegetableCountChangedEvent?.Invoke();

        }
        else
        {
            Debug.Log("El vegetal no fue entregado.");
        }

    }
    public void UseVegetable(Vegetable.VegetableType vegetableType)
    {
        switch (vegetableType)
        {
            case Vegetable.VegetableType.Beet:
                if (remolacha > 0) remolacha--;
                break;
            case Vegetable.VegetableType.Cauliflower:
                if (coliflor > 0) coliflor--;
                break;
        }
        Debug.Log("Beets: " + remolacha + ", Cauliflowers: " + coliflor);
        OnVegetableCountChangedEvent?.Invoke();

    }
}



