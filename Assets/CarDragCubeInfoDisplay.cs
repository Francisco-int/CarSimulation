using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarDragCubeInfoDisplay : MonoBehaviour
{
    [Header("Referencias")]
    public CarDragCube carDragCube; // Referencia al script 'CarDragCube' que maneja el cubo
    public Text infoText; // Componente UI para mostrar la información
    public InputField massInputField; // Campo de entrada para la masa del cubo
    public InputField springForceInputField; // Campo de entrada para la fuerza del resorte

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

        if (massInputField == null)
        {
            Debug.LogError("Por favor, asigna un Input Field para la masa.");
            return;
        }

        if (springForceInputField == null)
        {
            Debug.LogError("Por favor, asigna un Input Field para la fuerza del resorte.");
            return;
        }



        // Inicializar los valores de los Input Fields con los valores actuales
        massInputField.text = carDragCube.cubeMass.ToString();
        springForceInputField.text = carDragCube.springForce.ToString();

        // Configurar los listeners de los Input Fields
        massInputField.onEndEdit.AddListener(OnMassChanged);
        springForceInputField.onEndEdit.AddListener(OnSpringForceChanged);

        // Mostrar la información inicial
        UpdateInfoDisplay();
    }

    /// <summary>
    /// Actualiza la información mostrada en pantalla.
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

        // Mostrar la información en la UI
        infoText.text = $"Prueba: Arrastrar Cubo\nMasa del Cubo: {cubeMass:F1} kg\nFuerza del Resorte: {springForce:F1} N\nResistencia (Damper): {springDamper:F1}";
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
            carDragCube.cubeMass = newValue; // Actualizar la masa del cubo
            carDragCube.UpdateCubeProperties(carDragCube.cubeMass, carDragCube.springForce, carDragCube.springDamper, carDragCube.maxDistance); // Llamar a UpdateCubeProperties
        }
        else
        {
            Debug.LogWarning("Valor de masa no válido. Se mantiene el valor anterior.");
        }
    }

    /// <summary>
    /// Evento cuando se cambia la fuerza del resorte desde el Input Field.
    /// </summary>
    private void OnSpringForceChanged(string newSpringForce)
    {
        if (float.TryParse(newSpringForce, out float newValue))
        {
            carDragCube.springForce = newValue; // Actualizar la fuerza del resorte
            carDragCube.UpdateCubeProperties(carDragCube.cubeMass, carDragCube.springForce, carDragCube.springDamper, carDragCube.maxDistance); // Llamar a UpdateCubeProperties
        }
        else
        {
            Debug.LogWarning("Valor de fuerza del resorte no válido. Se mantiene el valor anterior.");
        }
    }
}

