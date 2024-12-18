using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarDragCubeInfoDisplay : MonoBehaviour
{
    [Header("Referencias")]
    public CarDragCube carDragCube; // Referencia al script 'CarDragCube' que maneja el cubo
    public Text infoText; // Componente UI para mostrar la informaci�n
    public InputField massInputField; // Campo de entrada para la masa del cubo
    public InputField springForceInputField; // Campo de entrada para la fuerza del resorte

    [Header("L�mites de valores")]
    public float massMin = 1f; // Masa m�nima
    public float massMax = 100f; // Masa m�xima
    public float springForceMin = 10f; // Fuerza m�nima del resorte
    public float springForceMax = 1000f; // Fuerza m�xima del resorte

    private void Start()
    {
        if (carDragCube == null)
        {
            Debug.LogError("Por favor, asigna el script 'CarDragCube' en el campo 'Car Drag Cube'.");
            return;
        }

        if (infoText == null)
        {
            Debug.LogError("Por favor, asigna un componente UI Text en el campo 'Info Text'.");
            return;
        }

        if (massInputField == null || springForceInputField == null)
        {
            Debug.LogError("Por favor, asigna los Input Fields en los campos correspondientes.");
            return;
        }

        // Inicializar los valores de los Input Fields con los valores actuales
        massInputField.text = carDragCube.cubeMass.ToString();
        springForceInputField.text = carDragCube.springForce.ToString();

        // Configurar los listeners de los Input Fields
        massInputField.onEndEdit.AddListener(OnMassChanged);
        springForceInputField.onEndEdit.AddListener(OnSpringForceChanged);

        // Mostrar la informaci�n inicial
        UpdateInfoDisplay();
    }

    /// <summary>
    /// Actualiza la informaci�n mostrada en pantalla.
    /// </summary>
    private void UpdateInfoDisplay()
    {
        // Obtener los datos del cubo
        Rigidbody cubeRb = carDragCube.cube.GetComponent<Rigidbody>();
        if (cubeRb == null)
        {
            Debug.LogError("El cubo no tiene Rigidbody.");
            return;
        }

        float cubeMass = cubeRb.mass; // Masa del cubo
        float springForce = carDragCube.springForce; // Fuerza del resorte
        float springDamper = carDragCube.springDamper; // Resistencia al movimiento

        // Mostrar la informaci�n en la UI
        infoText.text = $"Prueba: Arrastrar Cubo\nMasa del Cubo: {cubeMass:F1} kg\nFuerza del Resorte: {springForce:F1} N\nResistencia (Damper): {springDamper:F1}";
    }

    private void Update()
    {
        // Actualizar la informaci�n cada cuadro
        UpdateInfoDisplay();
    }

    /// <summary>
    /// Evento cuando se cambia la masa del cubo desde el Input Field.
    /// </summary>
    private void OnMassChanged(string newMass)
    {
        if (float.TryParse(newMass, out float newValue))
        {
            // Restringir el valor a los l�mites definidos
            newValue = Mathf.Clamp(newValue, massMin, massMax);

            // Actualizar la masa del cubo
            carDragCube.cubeMass = newValue;
            massInputField.text = newValue.ToString("F1"); // Asegurar que el campo muestra el valor clamped

            carDragCube.UpdateCubeProperties(carDragCube.cubeMass, carDragCube.springForce, carDragCube.springDamper, carDragCube.maxDistance);
        }
        else
        {
            Debug.LogWarning("Valor de masa no v�lido. Se mantiene el valor anterior.");
            massInputField.text = carDragCube.cubeMass.ToString("F1");
        }
    }

    /// <summary>
    /// Evento cuando se cambia la fuerza del resorte desde el Input Field.
    /// </summary>
    private void OnSpringForceChanged(string newSpringForce)
    {
        if (float.TryParse(newSpringForce, out float newValue))
        {
            // Restringir el valor a los l�mites definidos
            newValue = Mathf.Clamp(newValue, springForceMin, springForceMax);

            // Actualizar la fuerza del resorte
            carDragCube.springForce = newValue;
            springForceInputField.text = newValue.ToString("F1"); // Asegurar que el campo muestra el valor clamped

            carDragCube.UpdateCubeProperties(carDragCube.cubeMass, carDragCube.springForce, carDragCube.springDamper, carDragCube.maxDistance);
        }
        else
        {
            Debug.LogWarning("Valor de fuerza del resorte no v�lido. Se mantiene el valor anterior.");
            springForceInputField.text = carDragCube.springForce.ToString("F1");
        }
    }
}
