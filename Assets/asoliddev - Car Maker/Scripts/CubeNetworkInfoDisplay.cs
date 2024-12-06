using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeNetworkInfoDisplay : MonoBehaviour
{
    [Header("Referencias")]
    public CubeNetworkGenerator cubeNetworkGenerator; // Referencia al generador de red
    public Text infoText; // Componente UI donde se mostrará la información
    public InputField massInputField; // InputField para modificar la masa del cubo
    public InputField forceInputField; // InputField para modificar la fuerza de unión del joint

    [Header("Límites de valores")]
    public float massMin = 0.1f; // Masa mínima permitida para un cubo
    public float massMax = 100f; // Masa máxima permitida para un cubo
    public float forceMin = 10f; // Fuerza mínima permitida para los joints
    public float forceMax = 1000f; // Fuerza máxima permitida para los joints

    private float singleCubeMass = 0f;
    private float jointBreakForce = 0f;

    private void Start()
    {
        if (cubeNetworkGenerator == null)
        {
            Debug.LogError("Falta asignar el generador de cubos en el campo 'Cube Network Generator'.");
            return;
        }

        if (infoText == null)
        {
            Debug.LogError("Falta asignar el componente UI Text en el campo 'Info Text'.");
            return;
        }

        if (massInputField == null || forceInputField == null)
        {
            Debug.LogError("Falta asignar los InputFields en el Inspector.");
            return;
        }

        // Calcular la información inicial de un cubo
        FetchCubeNetworkInfo();

        // Mostrar la información
        UpdateInfoDisplay();

        // Configurar los valores de los InputFields
        massInputField.text = singleCubeMass.ToString("F1");
        forceInputField.text = jointBreakForce.ToString("F1");

        // Agregar listeners para actualizar los valores cuando se cambien en el InputField
        massInputField.onEndEdit.AddListener(OnMassInputChanged);
        forceInputField.onEndEdit.AddListener(OnForceInputChanged);
    }

    /// <summary>
    /// Obtiene el peso de un cubo y la fuerza de los joints.
    /// </summary>
    private void FetchCubeNetworkInfo()
    {
        singleCubeMass = cubeNetworkGenerator.cubeMass; // Peso definido en el generador
        jointBreakForce = cubeNetworkGenerator.breakForce; // Fuerza uniforme definida en el generador
    }

    /// <summary>
    /// Actualiza el texto mostrado en pantalla.
    /// </summary>
    private void UpdateInfoDisplay()
    {
        infoText.text = $"Prueba: Red de Cubos\n" +
                        $"Peso de un Cubo: {singleCubeMass:F1} kg\n" +
                        $"Fuerza de Unión por Joint: {jointBreakForce:F1} N";
    }

    /// <summary>
    /// Función llamada cuando se cambia la masa en el InputField.
    /// </summary>
    private void OnMassInputChanged(string value)
    {
        if (float.TryParse(value, out float newMass))
        {
            // Restringir el valor a los límites definidos
            newMass = Mathf.Clamp(newMass, massMin, massMax);
            singleCubeMass = newMass;

            // Actualizar propiedades en el generador
            cubeNetworkGenerator.UpdateCubeProperties(singleCubeMass, jointBreakForce);

            // Actualizar la UI
            massInputField.text = singleCubeMass.ToString("F1");
            UpdateInfoDisplay();
        }
        else
        {
            Debug.LogError("Entrada no válida para la masa.");
            massInputField.text = singleCubeMass.ToString("F1");
        }
    }

    /// <summary>
    /// Función llamada cuando se cambia la fuerza de unión en el InputField.
    /// </summary>
    private void OnForceInputChanged(string value)
    {
        if (float.TryParse(value, out float newForce))
        {
            // Restringir el valor a los límites definidos
            newForce = Mathf.Clamp(newForce, forceMin, forceMax);
            jointBreakForce = newForce;

            // Actualizar propiedades en el generador
            cubeNetworkGenerator.UpdateCubeProperties(singleCubeMass, jointBreakForce);

            // Actualizar la UI
            forceInputField.text = jointBreakForce.ToString("F1");
            UpdateInfoDisplay();
        }
        else
        {
            Debug.LogError("Entrada no válida para la fuerza de unión.");
            forceInputField.text = jointBreakForce.ToString("F1");
        }
    }
}
