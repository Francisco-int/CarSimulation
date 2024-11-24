using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeNetworkInfoDisplay : MonoBehaviour
{
    [Header("Referencias")]
    public CubeNetworkGenerator cubeNetworkGenerator; // Referencia al generador de red
    public Text infoText; // Componente UI donde se mostrar� la informaci�n
    public InputField massInputField; // InputField para modificar la masa del cubo
    public InputField forceInputField; // InputField para modificar la fuerza de uni�n del joint

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

        // Calcular la informaci�n inicial de un cubo
        FetchCubeNetworkInfo();

        // Mostrar la informaci�n
        UpdateInfoDisplay();

        // Configurar los valores de los InputFields
        massInputField.text = singleCubeMass.ToString();
        forceInputField.text = jointBreakForce.ToString();

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
                        $"Fuerza de Uni�n por Joint: {jointBreakForce:F1} N";
    }

    /// <summary>
    /// Funci�n llamada cuando se cambia la masa en el InputField.
    /// </summary>
    private void OnMassInputChanged(string value)
    {
        if (float.TryParse(value, out float newMass))
        {
            singleCubeMass = newMass;
            // Llamar al m�todo de CubeNetworkGenerator para actualizar la masa y regenerar la red
            cubeNetworkGenerator.UpdateCubeProperties(singleCubeMass, jointBreakForce);
            UpdateInfoDisplay();
        }
        else
        {
            Debug.LogError("Entrada no v�lida para la masa.");
        }
    }

    /// <summary>
    /// Funci�n llamada cuando se cambia la fuerza de uni�n en el InputField.
    /// </summary>
    private void OnForceInputChanged(string value)
    {
        if (float.TryParse(value, out float newForce))
        {
            jointBreakForce = newForce;
            // Llamar al m�todo de CubeNetworkGenerator para actualizar la fuerza y regenerar la red
            cubeNetworkGenerator.UpdateCubeProperties(singleCubeMass, jointBreakForce);
            UpdateInfoDisplay();
        }
        else
        {
            Debug.LogError("Entrada no v�lida para la fuerza de uni�n.");
        }
    }
}
