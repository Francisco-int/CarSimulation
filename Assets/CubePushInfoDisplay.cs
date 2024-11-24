using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubePushInfoDisplay : MonoBehaviour
{
    [Header("Referencias")]
    public CubePush cubePush; // Referencia al script 'CubePush' que maneja el cubo
    public Text infoText; // Componente UI para mostrar la información
    public InputField massInputField; // Campo de entrada para la masa del cubo
    public InputField accelerationInputField; // Campo de entrada para la aceleración del cubo

    private void Start()
    {
        if (cubePush == null)
        {
            Debug.LogError("Por favor, asigna el script 'CubePush' en el campo 'Cube Push'.");
            return;
        }

        if (infoText == null)
        {
            Debug.LogError("Por favor, asigna un componente UI Text en el campo 'Info Text'.");
            return;
        }

        if (massInputField == null)
        {
            Debug.LogError("Por favor, asigna un Input Field para la masa.");
            return;
        }

        if (accelerationInputField == null)
        {
            Debug.LogError("Por favor, asigna un Input Field para la aceleración.");
            return;
        }

        // Inicializar los valores de los Input Fields con los valores actuales
        massInputField.text = cubePush.cubeMass.ToString();
        accelerationInputField.text = cubePush.cubeAcceleration.ToString();

        // Configurar los listeners de los Input Fields
        massInputField.onEndEdit.AddListener(OnMassChanged);
        accelerationInputField.onEndEdit.AddListener(OnAccelerationChanged);

        // Mostrar la información inicial
        UpdateInfoDisplay();
    }

    /// <summary>
    /// Actualiza la información mostrada en pantalla.
    /// </summary>
    private void UpdateInfoDisplay()
    {
        // Obtener los datos del cubo
        Rigidbody cubeRb = cubePush.cube.GetComponent<Rigidbody>();
        if (cubeRb == null)
        {
            Debug.LogError("El cubo no tiene Rigidbody.");
            return;
        }

        float cubeMass = cubeRb.mass; // Masa del cubo
        float cubeSpeed = cubeRb.velocity.magnitude * 3.6f; // Convertir la velocidad de m/s a km/h (1 m/s = 3.6 km/h)

        // Mostrar la información en la UI
        infoText.text = $"Prueba: Empujar Cubo\nMasa del Cubo: {cubeMass:F1} kg\nVelocidad del Cubo: {cubeSpeed:F1} km/h";
    }

    void Update()
    {
        // Actualizar la información cada cuadro
        UpdateInfoDisplay();
    }

    /// <summary>
    /// Evento cuando se cambia la masa del cubo desde el Input Field.
    /// </summary>
    private void OnMassChanged(string newMass)
    {
        if (float.TryParse(newMass, out float newValue))
        {
            cubePush.cubeMass = newValue; // Actualizar la masa del cubo
        }
        else
        {
            Debug.LogWarning("Valor de masa no válido. Se mantiene el valor anterior.");
        }
    }

    /// <summary>
    /// Evento cuando se cambia la aceleración del cubo desde el Input Field.
    /// </summary>
    private void OnAccelerationChanged(string newAcceleration)
    {
        if (float.TryParse(newAcceleration, out float newValue))
        {
            cubePush.cubeAcceleration = newValue; // Actualizar la aceleración del cubo
        }
        else
        {
            Debug.LogWarning("Valor de aceleración no válido. Se mantiene el valor anterior.");
        }
    }
}


