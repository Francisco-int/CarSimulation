using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDragCube : MonoBehaviour
{
    public GameObject cube;  // El cubo que será arrastrado
    public float cubeMass = 5f; // Masa del cubo
    public float springForce = 50f; // Fuerza del resorte del Joint
    public float springDamper = 5f; // Resistencia al movimiento (damper)
    public float maxDistance = 5f; // Distancia máxima entre el cubo y el auto

    private Rigidbody carRb;  // Rigidbody del auto
    private Rigidbody cubeRb;  // Rigidbody del cubo
    private SpringJoint springJoint;  // SpringJoint que conecta el cubo al auto

    void Start()
    {
        // Obtener los Rigidbody
        carRb = GetComponent<Rigidbody>();
        cubeRb = cube.GetComponent<Rigidbody>();

        if (cubeRb == null || carRb == null)
        {
            Debug.LogError("Asegúrate de que el auto y el cubo tengan un Rigidbody.");
            return;
        }

        // Configurar la masa del cubo
        cubeRb.mass = cubeMass;

        // Crear y configurar el SpringJoint para conectar el cubo al auto
        springJoint = cube.AddComponent<SpringJoint>();  // Aquí agregamos el SpringJoint al cubo
        springJoint.connectedBody = carRb;  // El auto es el cuerpo al que está conectado el cubo
        springJoint.spring = springForce;  // Fuerza del resorte
        springJoint.damper = springDamper;  // Resistencia al movimiento
        springJoint.maxDistance = maxDistance;  // Distancia máxima entre el auto y el cubo
    }

    /// <summary>
    /// Actualiza la masa del cubo y las propiedades del SpringJoint en tiempo real.
    /// </summary>
    /// <param name="newMass">La nueva masa del cubo.</param>
    /// <param name="newSpringForce">La nueva fuerza del resorte.</param>
    /// <param name="newDamper">La nueva resistencia al movimiento.</param>
    /// <param name="newMaxDistance">La nueva distancia máxima entre el cubo y el auto.</param>
    public void UpdateCubeProperties(float newMass, float newSpringForce, float newDamper, float newMaxDistance)
    {
        // Actualizar la masa del cubo
        cubeRb.mass = newMass;

        // Actualizar las propiedades del SpringJoint
        springJoint.spring = newSpringForce;
        springJoint.damper = newDamper;
        springJoint.maxDistance = newMaxDistance;
    }
}

