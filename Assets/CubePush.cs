using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePush : MonoBehaviour
{
    public GameObject cube;  // Referencia al cubo
    public float cubeMass = 5f; // Masa del cubo
    public float cubeAcceleration = 10f; // Aceleraci�n del cubo

    // Vector de direcci�n que se podr� asignar en el Inspector
    public Vector3 accelerationDirection = new Vector3(0, 0, 0); // Por defecto se mueve en la direcci�n Z

    private Rigidbody cubeRb;  // Rigidbody del cubo

    void Start()
    {
        // Obtener el Rigidbody del cubo
        cubeRb = cube.GetComponent<Rigidbody>();

        if (cubeRb == null)
        {
            Debug.LogError("El cubo no tiene un Rigidbody. Aseg�rate de agregar un Rigidbody al cubo.");
            return;
        }

        // Configurar la masa del cubo
        cubeRb.mass = cubeMass;
    }

    void Update()
    {
        // Normalizar la direcci�n de aceleraci�n para que no afecte la magnitud de la aceleraci�n
        Vector3 normalizedDirection = accelerationDirection.normalized;

        // Calcular la fuerza a aplicar al cubo en la direcci�n definida por 'accelerationDirection'
        Vector3 accelerationForce = normalizedDirection * cubeAcceleration;

        // Aplicar la fuerza al cubo
        cubeRb.AddForce(accelerationForce, ForceMode.Force);

        // Muestra la velocidad del cubo en la consola
        Debug.Log("Velocidad del cubo: " + cubeRb.velocity.magnitude + " m/s");
    }
}
