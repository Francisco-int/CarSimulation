using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSpeedDisplay : MonoBehaviour
{
    /// <summary>
    /// Referencia al CarController del auto.
    /// </summary>
    public CarControler carControler;

    /// <summary>
    /// Texto UI donde se mostrará la información.
    /// </summary>
    public Text infoText;

    /// <summary>
    /// InputField para modificar la masa del auto.
    /// </summary>
    public InputField carMassInputField;

    /// <summary>
    /// Rigidbody del auto para obtener y modificar la masa.
    /// </summary>
    private Rigidbody carRigidbody;

    private void Start()
    {
        // Obtener el Rigidbody del auto
        if (carControler != null)
        {
            carRigidbody = carControler.GetComponent<Rigidbody>();
        }

        // Validar el InputField
        if (carMassInputField == null)
        {
            Debug.LogError("Por favor, asigna un Input Field para la masa del auto.");
            return;
        }

        if (carRigidbody != null)
        {
            // Inicializar el valor del InputField con la masa actual del auto
            carMassInputField.text = carRigidbody.mass.ToString();

            // Configurar el listener para el InputField
            carMassInputField.onEndEdit.AddListener(OnCarMassChanged);
        }
    }

    private void Update()
    {
        if (carControler != null && infoText != null && carRigidbody != null)
        {
            // Convertir la velocidad a km/h
            float speedKmH = carControler.speed * 3.6f;

            // Obtener la masa del auto
            float carMass = carRigidbody.mass;

            // Mostrar la información en el texto
            infoText.text = $"Velocidad: {speedKmH:F1} km/h\nMasa: {carMass:F1} kg";
        }
    }

    /// <summary>
    /// Evento cuando se cambia la masa del auto desde el Input Field.
    /// </summary>
    private void OnCarMassChanged(string newMass)
    {
        if (float.TryParse(newMass, out float newValue))
        {
            if (newValue > 0f)
            {
                carRigidbody.mass = newValue; // Actualizar la masa del Rigidbody
            }
            else
            {
                Debug.LogWarning("La masa debe ser mayor a 0.");
                carMassInputField.text = carRigidbody.mass.ToString(); // Revertir el valor al actual
            }
        }
        else
        {
            Debug.LogWarning("Valor de masa no válido. Se mantiene el valor anterior.");
            carMassInputField.text = carRigidbody.mass.ToString(); // Revertir el valor al actual
        }
    }
}

