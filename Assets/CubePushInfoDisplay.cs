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

    [Header("Límites de valores")]
    public float massMin = 1f; // Masa mínima
    public float massMax = 100f; // Masa máxima
    public float accelerationMin = 0.1f; // Aceleración mínima
    public float accelerationMax = 50f; // Aceleración máxima

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

        if (massInputField == null || accelerationInputField == null)
        {
            Debug.LogError("Por favor, asigna los Input Fields en los campos correspondientes.");
            return;
        }

        // Inicializar los valores de los Input Fields con los valores actuales
        massInputField.text = cubePush.cubeMass.ToString("F1");
        accelerationInputField.text = cubePush.cubeAcceleration.ToString("F1");

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

    private void Update()
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
            // Restringir el valor a los límites definidos
            newValue = Mathf.Clamp(newValue, massMin, massMax);

            // Actualizar la masa del cubo
            cubePush.cubeMass = newValue;
            Rigidbody cubeRb = cubePush.cube.GetComponent<Rigidbody>();
            cubeRb.mass = newValue;

            // Asegurar que el Input Field muestra el valor clamped
            massInputField.text = newValue.ToString("F1");
        }
        else
        {
            Debug.LogWarning("Valor de masa no válido. Se mantiene el valor anterior.");
            massInputField.text = cubePush.cubeMass.ToString("F1");
        }
    }

    /// <summary>
    /// Evento cuando se cambia la aceleración del cubo desde el Input Field.
    /// </summary>
    private void OnAccelerationChanged(string newAcceleration)
    {
        if (float.TryParse(newAcceleration, out float newValue))
        {
            // Restringir el valor a los límites definidos
            newValue = Mathf.Clamp(newValue, accelerationMin, accelerationMax);

            // Actualizar la aceleración del cubo
            cubePush.cubeAcceleration = newValue;

            // Asegurar que el Input Field muestra el valor clamped
            accelerationInputField.text = newValue.ToString("F1");
        }
        else
        {
            Debug.LogWarning("Valor de aceleración no válido. Se mantiene el valor anterior.");
            accelerationInputField.text = cubePush.cubeAcceleration.ToString("F1");
        }
    }
}
