using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeNetworkGenerator : MonoBehaviour
{
    public GameObject cubePrefab; // Prefab del cubo a usar
    public Transform originTransform; // Transform desde donde se generará la red
    public int rows = 5; // Número de filas
    public int columns = 5; // Número de columnas
    public float spacing = 1.1f; // Espaciado entre los cubos
    public float breakForce = 500f; // Resistencia máxima de los joints
    public float cubeMass = 1f; // Masa de cada cubo

    // Matriz para almacenar los cubos generados
    private GameObject[,] cubes;

    void Start()
    {
        GenerateCubeNetwork();
    }

    void GenerateCubeNetwork()
    {
        // Validar que se haya asignado un Transform de origen
        if (originTransform == null)
        {
            Debug.LogError("Por favor, asigna un objeto vacío en el campo 'Origin Transform'.");
            return;
        }

        // Inicializar la matriz de cubos
        cubes = new GameObject[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Calcular la posición del cubo en relación al transform de origen
                Vector3 localPosition = new Vector3(j * spacing, i * spacing, 0);
                Vector3 worldPosition = originTransform.TransformPoint(localPosition);

                // Crear el cubo
                GameObject cube = Instantiate(cubePrefab, worldPosition, originTransform.rotation, transform);

                // Configuración del Rigidbody
                Rigidbody rb = cube.AddComponent<Rigidbody>();
                rb.mass = cubeMass; // Asignar la masa definida en el Inspector
                rb.isKinematic = false; // Asegurarse de que no sea kinemático

                // Guardar el cubo en la matriz
                cubes[i, j] = cube;

                // Conectar con el cubo superior si existe
                if (i > 0)
                {
                    ConnectCubes(cube, cubes[i - 1, j]);
                }

                // Conectar con el cubo izquierdo si existe
                if (j > 0)
                {
                    ConnectCubes(cube, cubes[i, j - 1]);
                }
            }
        }
    }

    void ConnectCubes(GameObject cubeA, GameObject cubeB)
    {
        FixedJoint joint = cubeA.AddComponent<FixedJoint>();
        joint.connectedBody = cubeB.GetComponent<Rigidbody>();
        joint.breakForce = breakForce; // Resistencia máxima antes de romperse
        joint.breakTorque = breakForce; // Resistencia al torque antes de romperse
    }

    /// <summary>
    /// Método para actualizar la masa y la fuerza de unión de los cubos sin instanciarlos nuevamente.
    /// </summary>
    public void UpdateCubeProperties(float newMass, float newBreakForce)
    {
        cubeMass = newMass;
        breakForce = newBreakForce;

        // Actualizar la masa de todos los cubos existentes
        foreach (var cube in cubes)
        {
            if (cube != null)
            {
                Rigidbody rb = cube.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.mass = newMass; // Actualizar la masa del cubo
                }
            }
        }

        // Actualizar la fuerza de los joints
        foreach (var cube in cubes)
        {
            if (cube != null)
            {
                // Buscar todos los FixedJoints en cada cubo
                FixedJoint[] joints = cube.GetComponents<FixedJoint>();
                foreach (var joint in joints)
                {
                    if (joint != null)
                    {
                        joint.breakForce = newBreakForce; // Actualizar la fuerza de ruptura del joint
                        joint.breakTorque = newBreakForce; // Actualizar la fuerza de ruptura del torque del joint
                    }
                }
            }
        }
    }
}


