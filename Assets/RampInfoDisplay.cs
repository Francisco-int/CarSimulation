using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RampInfoDisplay : MonoBehaviour
{
    [Header("Referencias")]
    public RampPlacer rampPlacer; // Referencia al script que coloca las rampas
    public Text infoText; // Componente UI para mostrar la información
    public InputField distanceInputField; // Campo de entrada para la distancia entre rampas

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
        distanceInputField.text = rampPlacer.distanceBetweenRamps.ToString();

        // Configurar el listener del Input Field
        distanceInputField.onEndEdit.AddListener(OnDistanceChanged);

        // Mostrar la información inicial
        UpdateInfoDisplay();
    }

    /// <summary>
    /// Actualiza la información mostrada en pantalla.
    /// </summary>
    private void UpdateInfoDisplay()
    {
        // Datos relevantes de las rampas
        string ramp1Position = $"Rampa 1: ({rampPlacer.ramp1.position.x:F1}, {rampPlacer.ramp1.position.y:F1}, {rampPlacer.ramp1.position.z:F1})";
        string distanceBetweenRamps = $"Distancia entre Rampas: {rampPlacer.distanceBetweenRamps:F1} metros";

        // Mostrar información
        infoText.text = $"Prueba: Rampas\n{ramp1Position}\n{distanceBetweenRamps}";
    }

    void Update()
    {
        // Actualizar la información cada cuadro
        UpdateInfoDisplay();
    }

    /// <summary>
    /// Evento cuando se cambia la distancia entre rampas desde el Input Field.
    /// </summary>
    private void OnDistanceChanged(string newDistance)
    {
        if (float.TryParse(newDistance, out float newValue))
        {
            rampPlacer.distanceBetweenRamps = newValue; // Actualizar la distancia entre rampas
            rampPlacer.UpdateRampPositions(); // Actualizar las posiciones de las rampas después de cambiar la distancia
        }
        else
        {
            Debug.LogWarning("Valor de distancia no válido. Se mantiene el valor anterior.");
        }
    }
}



