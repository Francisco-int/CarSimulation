using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePush : MonoBehaviour
{
    public GameObject cube;  // Referencia al cubo
    public float cubeMass = 5f; // Masa del cubo
    public float cubeAcceleration = 10f; // Aceleración del cubo

    // Vector de dirección que se podrá asignar en el Inspector
    public Vector3 accelerationDirection = new Vector3(0, 0, 0); // Por defecto se mueve en la dirección Z

    private Rigidbody cubeRb;  // Rigidbody del cubo

    void Start()
    {
        // Obtener el Rigidbody del cubo
        cubeRb = cube.GetComponent<Rigidbody>();

        if (cubeRb == null)
        {
            Debug.LogError("El cubo no tiene un Rigidbody. Asegúrate de agregar un Rigidbody al cubo.");
            return;
        }

        // Configurar la masa del cubo
        cubeRb.mass = cubeMass;
    }

    void Update()
    {
        // Normalizar la dirección de aceleración para que no afecte la magnitud de la aceleración
        Vector3 normalizedDirection = accelerationDirection.normalized;

        // Calcular la fuerza a aplicar al cubo en la dirección definida por 'accelerationDirection'
        Vector3 accelerationForce = normalizedDirection * cubeAcceleration;

        // Aplicar la fuerza al cubo
        cubeRb.AddForce(accelerationForce, ForceMode.Force);

        // Muestra la velocidad del cubo en la consola
        Debug.Log("Velocidad del cubo: " + cubeRb.velocity.magnitude + " m/s");
    }
}
