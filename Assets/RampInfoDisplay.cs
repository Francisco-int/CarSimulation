using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RampInfoDisplay : MonoBehaviour
{
    [Header("Referencias")]
    public RampPlacer rampPlacer; // Referencia al script que coloca las rampas
    public Text infoText; // Componente UI para mostrar la informaci�n
    public InputField distanceInputField; // Campo de entrada para la distancia entre rampas

    [Header("L�mites de valores")]
    public float distanceMin = 1f; // Distancia m�nima entre rampas
    public float distanceMax = 100f; // Distancia m�xima entre rampas

    private void Start()
    {
        if (rampPlacer == null)
        {
            Debug.LogError("Por favor, asigna el script 'RampPlacer' en el campo 'Ramp Placer'.");
            return;
        }

        if (infoText == null)
        {
            Debug.LogError("Por favor, asigna un componente UI Text en el campo 'Info Text'.");
            return;
        }

        if (distanceInputField == null)
        {
            Debug.LogError("Por favor, asigna un Input Field para la distancia entre las rampas.");
            return;
        }

        // Inicializar el valor de la distancia con el valor actual
        distanceInputField.text = rampPlacer.distanceBetweenRamps.ToString("F1");

        // Configurar el listener del Input Field
        distanceInputField.onEndEdit.AddListener(OnDistanceChanged);

        // Mostrar la informaci�n inicial
        UpdateInfoDisplay();
    }

    /// <summary>
    /// Actualiza la informaci�n mostrada en pantalla.
    /// </summary>
    private void UpdateInfoDisplay()
    {
        // Datos relevantes de las rampas
        string ramp1Position = $"Rampa 1: ({rampPlacer.ramp1.position.x:F1}, {rampPlacer.ramp1.position.y:F1}, {rampPlacer.ramp1.position.z:F1})";
        string distanceBetweenRamps = $"Distancia entre Rampas: {rampPlacer.distanceBetweenRamps:F1} metros";

        // Mostrar informaci�n
        infoText.text = $"Prueba: Rampas\n{ramp1Position}\n{distanceBetweenRamps}";
    }

    private void Update()
    {
        // Actualizar la informaci�n cada cuadro
        UpdateInfoDisplay();
    }

    /// <summary>
    /// Evento cuando se cambia la distancia entre rampas desde el Input Field.
    /// </summary>
    private void OnDistanceChanged(string newDistance)
    {
        if (float.TryParse(newDistance, out float newValue))
        {
            // Restringir el valor a los l�mites definidos
            newValue = Mathf.Clamp(newValue, distanceMin, distanceMax);

            // Actualizar la distancia entre rampas
            rampPlacer.distanceBetweenRamps = newValue;

            // Actualizar las posiciones de las rampas despu�s de cambiar la distancia
            rampPlacer.UpdateRampPositions();

            // Asegurar que el Input Field muestra el valor clamped
            distanceInputField.text = newValue.ToString("F1");
        }
        else
        {
            Debug.LogWarning("Valor de distancia no v�lido. Se mantiene el valor anterior.");
            distanceInputField.text = rampPlacer.distanceBetweenRamps.ToString("F1");
        }
    }
}
